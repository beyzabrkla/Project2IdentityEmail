using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.DTOs;
using Project2IdentityEmail.Entities;
using System.Text.RegularExpressions;

namespace Project2IdentityEmail.Controllers
{
    [Authorize]
    public class MailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EMailContext _context;

        public MailController(UserManager<AppUser> userManager, EMailContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        #region GELEN KUTUSU
        public async Task<IActionResult> Inbox(string searchTerm, string folder = "inbox", string label = null, string filter = null, int page = 1)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await PrepareViewBags(user);
            ViewBag.CurrentUserName = user.UserName;
            ViewBag.CurrentUserId = user.Id;

            var query = _context.UserMessages
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Category)
                .AsQueryable();

            // --- MERKEZİ ARAMA MOTORU ---
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x =>
                    x.Subject.ToLower().Contains(searchTerm) ||
                    x.Content.ToLower().Contains(searchTerm) ||
                    x.Sender.UserName.ToLower().Contains(searchTerm) ||
                    x.Receiver.UserName.ToLower().Contains(searchTerm)
                );
                ViewBag.SearchTerm = searchTerm;
            }


            // KATEGORİ FİLTRESİ
            if (!string.IsNullOrEmpty(label))
            {
                query = query.Where(x => (x.ReceiverId == user.Id || x.SenderId == user.Id)
                                         && x.Category.CategoryName.ToLower() == label.ToLower()
                                         && !x.IsTrash
                                         && !x.IsSpam);
                ViewBag.Title = label;
                ViewBag.IsCategoryView = true;
            }
            else
            {
                ViewBag.IsCategoryView = false;
                switch (folder.ToLower())
                {
                    case "drafts":
                        query = query.Where(x => x.SenderId == user.Id && x.IsDraft && !x.IsTrash);
                        ViewBag.Title = "Taslaklar";
                        break;
                    case "starred":
                        query = query.Where(x => (x.ReceiverId == user.Id || x.SenderId == user.Id) && x.IsStarred && !x.IsTrash && !x.IsSpam);
                        ViewBag.Title = "Yıldızlı Mesajlar";
                        break;
                    case "trash":
                        query = query.Where(x => (x.ReceiverId == user.Id || x.SenderId == user.Id) && x.IsTrash);
                        ViewBag.Title = "Çöp Kutusu";
                        break;
                    case "spam":
                        query = query.Where(x => x.ReceiverId == user.Id && !x.IsTrash && x.IsSpam && !x.IsDraft);
                        ViewBag.Title = "Spam";
                        break;
                    case "unread":
                        query = query.Where(x => x.ReceiverId == user.Id && !x.IsRead && !x.IsTrash && !x.IsSpam);
                        ViewBag.Title = "Okunmamış Mesajlar";
                        break;
                    case "important":
                        query = query.Where(x => x.Category.CategoryName == "Önemli" && !x.IsTrash && !x.IsSpam);
                        ViewBag.Title = "Önemli Mesajlar";
                        break;
                    case "hasattachment":
                        query = query.Where(x => !string.IsNullOrEmpty(x.AttachmentUrl) && !x.IsSpam);
                        ViewBag.Title = "Ekli Dosyalar";
                        break;
                    default:
                        query = query.Where(x => x.ReceiverId == user.Id && !x.IsDraft && !x.IsTrash && !x.IsSpam);
                        ViewBag.Title = "Gelen Kutusu";
                        break;
                }
            }

            if (filter == "unread")
                query = query.Where(x => !x.IsRead && !x.IsSpam);

            // Toplam kayıt sayısını filtreleme yapıldıktan SONRA alalım
            int totalCount = await query.CountAsync();
            ViewBag.TotalCount = totalCount;

            // Basit bir 1-50 gösterimi için (Gerçek pagination istersen .Skip().Take() ekleriz)
            ViewBag.RangeInfo = totalCount > 0 ? $"1-{Math.Min(50, totalCount)} / {totalCount}" : "0-0 / 0";

            var values = await query.OrderByDescending(x => x.Date).ToListAsync();
            ViewBag.CurrentFolder = folder;


            return View(values);
        }
        #endregion


        #region GİDEN KUTUSU
        public async Task<IActionResult> Outbox(string searchTerm)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await PrepareViewBags(user);


            ViewBag.CurrentFolder = "outbox"; // Giden kutusu için
            ViewBag.Title = "Giden Kutusu";   // Giden kutusu başlığı
            ViewBag.IsCategoryView = false; // Giden kutusunda kategori filtresi yok

            var query = _context.UserMessages
                .Where(x => x.SenderId == user.Id && !x.IsTrash && !x.IsDraft) // Giden kutusu için filtreleme kullanıcı idsi çöp kutusunda olmaması ve taslak olmaması
                .Include(x => x.Receiver) //alıcı
                .Include(x => x.Category) //kategori
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm)) // Arama terimi varsa filtreleme yap
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(x =>
                    x.Subject.ToLower().Contains(searchTerm) ||
                    x.Content.ToLower().Contains(searchTerm) ||
                    x.Receiver.UserName.ToLower().Contains(searchTerm)
                );
                ViewBag.SearchTerm = searchTerm;
            }

            // Sayfalama (Pagination) için gereken ViewBag'leri de ekleyelim (View'da çağırıyorsun)
            int totalCount = await query.CountAsync();
            ViewBag.TotalCount = totalCount;
            ViewBag.RangeInfo = totalCount > 0 ? $"1-{Math.Min(50, totalCount)} / {totalCount}" : "0-0 / 0";

            var values = await query.OrderByDescending(x => x.Date).ToListAsync();
            return View(values);
        }
        #endregion
        

        #region  SİLME VE KURTARMA İŞLEMLERİ
        // 1. Çöpe Taşı (Soft Delete)
        public async Task<IActionResult> MoveToTrash(int id)
        {
            var value = await _context.UserMessages.FindAsync(id);
            if (value != null)
            {
                value.IsTrash = true;
                value.IsStarred = false;
                value.IsSpam = false;
                await _context.SaveChangesAsync();
            }
            var returnUrl = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Inbox");
        }

        // 2. Çöpten Kurtar
        public async Task<IActionResult> RestoreFromTrash(int id)
        {
            var value = await _context.UserMessages.FindAsync(id);
            if (value != null)
            {
                value.IsTrash = false;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Inbox", new { folder = "trash" });
        }

        // 3. KALICI SİL (Hard Delete)
        public async Task<IActionResult> HardDelete(int id)
        {
            var value = await _context.UserMessages.FindAsync(id);
            if (value != null)
            {
                _context.UserMessages.Remove(value);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Inbox", new { folder = "trash" });
        }
        #endregion


        #region MAİL DETAY
        public async Task<IActionResult> MailDetails(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await PrepareViewBags(user);

            ViewBag.IsCategoryView = false; // Sidebar'daki kategori kontrolü için şart
            ViewBag.CurrentFolder = "none";
            ViewBag.Title = "Mesaj Detayı";
            // -------------------------------------------

            var value = await _context.UserMessages
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (value == null) return NotFound();

            if (!value.IsRead && value.Receiver?.UserName == User.Identity.Name)
            {
                value.IsRead = true;
                await _context.SaveChangesAsync();
            }
            return View(value);
        }
        #endregion


        #region YENİ MAİL OLUŞTURMA

        [HttpGet]
        public async Task<IActionResult> ComposeMail(int? id, string receiverMail = null, string subject = null)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await PrepareViewBags(user);

            ViewBag.IsCategoryView = false;
            ViewBag.CurrentFolder = "compose";
            ViewBag.Categories = _context.Categories.ToList();

            // 1. Durum: Taslak Düzenleme
            if (id.HasValue)
            {
                var draft = await _context.UserMessages
                    .Include(x => x.Receiver)
                    .FirstOrDefaultAsync(x => x.Id == id.Value);

                if (draft != null)
                {
                    var model = new SendMessageDTO
                    {
                        Id = draft.Id,
                        ReceiverEmail = draft.Receiver?.Email,
                        Subject = draft.Subject,
                        Content = draft.Content,
                        CategoryId = draft.CategoryId ?? 0
                    };
                    return View(model);
                }
            }

            // --- RE: KONTROLÜ (GET TARAFI - Arayüz için) ---
            if (!string.IsNullOrEmpty(subject))
            {
                // Regex: "Re:" kelimesini ve yanındaki tüm boşlukları bulur, büyük/küçük harf bakmaz.
                string pattern = @"(?i)Re:\s*";
                string cleanSubject = Regex.Replace(subject, pattern, "").Trim();
                subject = "Re: " + cleanSubject;
            }

            ViewBag.ReceiverMail = receiverMail;
            ViewBag.Subject = subject;
            ViewBag.Content = string.IsNullOrEmpty(receiverMail) ? "" : "<br><br>----------<br>Yanıtlanan mesaj:";

            return View(new SendMessageDTO { Subject = subject, ReceiverEmail = receiverMail });
        }

        [HttpPost]
        public async Task<IActionResult> ComposeMail(SendMessageDTO model, string actionType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool isDraft = (actionType == "saveDraft");
            var receiver = await _userManager.FindByEmailAsync(model.ReceiverEmail);

            if (!isDraft && receiver == null)
            {
                ModelState.AddModelError("", "Alıcı e-posta adresi bulunamadı.");
                ViewBag.Categories = _context.Categories.ToList();
                return View("ComposeMail", model);
            }

            // --- RE: KONTROLÜ (POST TARAFI - Veritabanı için) ---
            // Kullanıcı formu gönderdiğinde başlıkta birden fazla Re: varsa hepsini teke düşürür
            if (!string.IsNullOrEmpty(model.Subject))
            {
                string pattern = @"(?i)Re:\s*";
                string cleanSubject = Regex.Replace(model.Subject, pattern, "").Trim();
                model.Subject = "Re: " + cleanSubject;
            }

            // --- DOSYA İŞLEMLERİ ---
            string attachmentPath = null;
            if (model.Attachment != null && model.Attachment.Length > 0)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Attachment.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Attachment.CopyToAsync(stream);
                }
                attachmentPath = "/Attachments/" + fileName;
            }

            // --- ESKİ TASLAĞI SİLME ---
            if (model.Id > 0)
            {
                var oldDraft = await _context.UserMessages.FindAsync(model.Id);
                if (oldDraft != null) _context.UserMessages.Remove(oldDraft);
            }

            // --- MESAJI KAYDET ---
            var message = new UserMessage
            {
                SenderId = user.Id,
                ReceiverId = receiver?.Id,
                Subject = model.Subject ?? "(Konu Yok)",
                Content = model.Content ??"",
                CategoryId = (model.CategoryId > 0) ? model.CategoryId : (int?)null,
                Date = DateTime.Now,
                IsStarred = false,
                IsRead = false,
                IsDraft = isDraft,
                IsTrash = false,
                AttachmentUrl = attachmentPath
            };

            _context.UserMessages.Add(message);
            await _context.SaveChangesAsync();

            return isDraft
                ? RedirectToAction("Inbox", new { folder = "drafts" })
                : RedirectToAction("Outbox");
        }
        #endregion


        #region SPAM İŞLEMLERİ
        public async Task<IActionResult> MoveToSpam(int id)
        {
            var value = await _context.UserMessages.FindAsync(id);
            if (value != null)
            {
                value.IsSpam = true;
                value.IsStarred = false;
                await _context.SaveChangesAsync();
            }
            var returnUrl = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Inbox");
        }

        public async Task<IActionResult> RestoreFromSpam(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var message = await _context.UserMessages.FindAsync(id);
            if (message != null)
            {
                message.IsSpam = false;
                await _context.SaveChangesAsync();
                return message.ReceiverId == user.Id ? RedirectToAction("Inbox") : RedirectToAction("Outbox");
            }
            return RedirectToAction("Inbox", new { folder = "spam" });
        }

        #endregion


        #region SAYAÇ HAZIRLAMA
        private async Task PrepareViewBags(AppUser user)
        {
            // Gelen Kutusu: Okunmamış + Taslak Değil + Çöpte Değil + Spam Değil
            ViewBag.InboxCount = await _context.UserMessages.CountAsync(x => x.ReceiverId == user.Id && !x.IsRead && !x.IsDraft && !x.IsTrash && !x.IsSpam);

            // Giden Kutusu: Gönderen + Çöpte Değil + Taslak Değil
            ViewBag.OutboxCount = await _context.UserMessages.CountAsync(x => x.SenderId == user.Id && !x.IsTrash && !x.IsDraft);

            // Taslaklar: Taslak olarak işaretlenmiş + Çöpte Değil
            ViewBag.DraftCount = await _context.UserMessages.CountAsync(x => x.SenderId == user.Id && x.IsDraft && !x.IsTrash);

            // Yıldızlılar: Kullanıcıyla ilgili (gelen veya giden) + Yıldızlı + Çöpte Değil
            ViewBag.StarredCount = await _context.UserMessages.CountAsync(x => (x.ReceiverId == user.Id || x.SenderId == user.Id) && x.IsStarred && !x.IsTrash);

            // Spam: Kullanıcıya gelen + Spam + Çöpte Değik
            ViewBag.SpamCount = await _context.UserMessages.CountAsync(x => x.ReceiverId == user.Id && x.IsSpam && !x.IsTrash);

            // Çöp Kutusu: Kullanıcıyla ilgili (gelen veya giden) + Çöpte olan
            ViewBag.TrashCount = await _context.UserMessages.CountAsync(x => (x.ReceiverId == user.Id || x.SenderId == user.Id) && x.IsTrash);

            // Kategoriler
            ViewBag.Categories = await _context.Categories.ToListAsync();

            // Mevcut Kullanıcı Adı
            ViewBag.CurrentUserName = user.UserName;


            // Üst bar için okunmamış mesaj sayısı
            ViewBag.MessageCount = await _context.UserMessages.CountAsync(x => x.ReceiverId == user.Id && !x.IsRead && !x.IsTrash && !x.IsDraft && !x.IsSpam);

            // Üst bar açılır menüsü için son 5 mesajın ön gösterimi
            ViewBag.LastMessages = await _context.UserMessages
                .Where(x => x.ReceiverId == user.Id && !x.IsTrash && !x.IsDraft && !x.IsSpam)
                .OrderByDescending(x => x.Date)
                .Take(5)
                .Select(x => new {
                    Id = x.Id,
                    SenderName = x.Sender.Name + " " + x.Sender.Surname,
                    ShortContent = x.Content.Length > 35 ? x.Content.Substring(0, 35) + "..." : x.Content,
                    SendTime = x.Date.ToString("HH:mm"),
                    UserImageUrl = x.Sender.ImageUrl,
                    IsRead = x.IsRead
                }).ToListAsync();
        }
        #endregion


        #region KATEGORİLERİ GETİR
        public async Task<IActionResult> CategoryFilter(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Hem gönderen hem alıcı olduğumuz, seçili kategorideki mailler
            var messages = _context.UserMessages
                .Include(x => x.Sender).Include(x => x.Receiver).Include(x => x.Category)
                .Where(x => x.CategoryId == id && (x.SenderId == user.Id || x.ReceiverId == user.Id) && !x.IsSpam && !x.IsTrash)
                .OrderByDescending(x => x.Date)
                .ToList();

            ViewBag.CategoryName = _context.Categories.Find(id)?.CategoryName;
            ViewBag.Categories = _context.Categories.ToList(); // Sidebar için
            ViewBag.CurrentUserId = user.Id;

            return View(messages);
        }

        // Kategori Atama Metodu
        public IActionResult AssignCategory(int mailId, int categoryId)
        {
            var mail = _context.UserMessages.Find(mailId);
            if (mail != null)
            {
                mail.CategoryId = categoryId;
                _context.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion


        #region YILDIZ İŞLEMLERİ

        [HttpPost]
        public async Task<IActionResult> ToggleStar(int id)
        {
            var message = await _context.UserMessages.FindAsync(id);
            if (message == null) return NotFound();

            // Yıldız durumunu tersine çevir (true ise false, false ise true)
            message.IsStarred = !message.IsStarred;
            await _context.SaveChangesAsync();

            return Json(new { success = true, isStarred = message.IsStarred });
        }
        #endregion
    }
}