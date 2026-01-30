using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.DTOs;
using Project2IdentityEmail.Entities;

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
        public async Task<IActionResult> Inbox(string folder = "inbox", string label = null, string filter = null)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.CurrentUserName = user.UserName; 
            var query = _context.UserMessages
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.Category)
                .AsQueryable();

            switch (folder.ToLower())
            {
                case "drafts":
                    query = query.Where(x => x.SenderId == user.Id && x.IsDraft && !x.IsTrash);
                    ViewBag.Title = "Taslaklar";
                    break;
                case "starred":
                    query = query.Where(x => (x.ReceiverId == user.Id || x.SenderId == user.Id) && x.IsStarred && !x.IsTrash);
                    ViewBag.Title = "Yıldızlı Mesajlar";
                    break;
                case "trash":
                    // ÇÖP KUTUSU: Hem gelen hem giden ama IsTrash olanlar
                    query = query.Where(x => (x.ReceiverId == user.Id || x.SenderId == user.Id) && x.IsTrash);
                    ViewBag.Title = "Çöp Kutusu";
                    break;
                case "spam":
                    query = query.Where(x => x.ReceiverId == user.Id && x.IsSpam && !x.IsTrash);
                    ViewBag.Title = "Spam";
                    break;
                default:
                    query = query.Where(x => x.ReceiverId == user.Id && !x.IsDraft && !x.IsTrash && !x.IsSpam);
                    ViewBag.Title = "Gelen Kutusu";
                    break;
            }

            if (!string.IsNullOrEmpty(label))
                query = query.Where(x => x.Category.CategoryName.ToLower() == label.ToLower());

            if (filter == "unread")
                query = query.Where(x => !x.IsRead);

            var values = await query.OrderByDescending(x => x.Date).ToListAsync();
            ViewBag.CurrentFolder = folder;
            return View(values);
        }
        #endregion


        #region GİDEN KUTUSU
        public async Task<IActionResult> Outbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.CurrentFolder = "outbox";
            var values = await _context.UserMessages
                .Where(x => x.SenderId == user.Id && !x.IsTrash && !x.IsDraft)
                .Include(x => x.Receiver)
                .Include(x => x.Category)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
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
        public IActionResult ComposeMail(string receiverMail = null, string subject = null)
        {
            ViewBag.Categories = _context.Categories.ToList();

            // Eğer yanıtla butonundan gelmişse bu ViewBag'ler dolu gider, Yeni Mesaj ise null gider
            ViewBag.ReceiverMail = receiverMail;
            ViewBag.Subject = subject;

            // Yanıtla senaryosunda içeriğe ufak bir not eklemek istersen:
            ViewBag.Content = string.IsNullOrEmpty(receiverMail) ? "" : "<br><br>----------<br>Yanıtlanan mesaj:";

            return View();
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

            // --- DOSYA YÜKLEME MANTIĞI ---
            string attachmentPath = null;
            if (model.Attachment != null && model.Attachment.Length > 0)
            {
                // wwwroot içinde 'attachments' klasörü yoksa oluşturun
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments");
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                // Benzersiz bir dosya adı oluşturun
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Attachment.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Attachment.CopyToAsync(stream);
                }
                attachmentPath = "/attachments/" + fileName; // Veritabanına kaydedilecek yol
            }

            var message = new UserMessage
            {
                SenderId = user.Id,
                ReceiverId = receiver?.Id,
                Subject = model.Subject ?? "(Konu Yok)",
                Content = model.Content,
                CategoryId = (model.CategoryId > 0) ? model.CategoryId : (int?)null,
                Date = DateTime.Now,
                IsRead = false,
                IsDraft = isDraft,
                IsTrash = false,
                IsStarred = false,
                IsSpam = false,
                AttachmentUrl = attachmentPath 
            };

            _context.UserMessages.Add(message);
            await _context.SaveChangesAsync();

            return isDraft ? RedirectToAction("Inbox", new { folder = "drafts" }) : RedirectToAction("Outbox");
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
    }
}