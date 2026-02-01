using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EMailContext _context;

        public ProfileController(UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, EMailContext context)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Okunmamış gelen mesajlar
            ViewBag.UnreadCount = _context.UserMessages
                .Count(x => x.ReceiverId == user.Id && !x.IsRead && !x.IsTrash && !x.IsDraft);

            // Tüm gelen ve giden trafiği (Etkileşim)
            ViewBag.TotalInteraction = _context.UserMessages
                .Count(x => x.ReceiverId == user.Id || x.SenderId == user.Id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUser model, IFormFile? ProfilePicture)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcı bilgilerini güncelle
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            user.City = model.City;
            user.About = model.About;
            user.JobTitle = model.JobTitle;

            // yeni resim yükleme
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                // Eski resmi sil
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserImages", user.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni resmi kaydet
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserImages", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await ProfilePicture.CopyToAsync(stream);
                }

                user.ImageUrl = fileName;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profiliniz başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Index", user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await _userManager.GetUserAsync(User); // Şu anki kullanıcıyı al

            if (user == null) // Kullanıcı bulunamazsa
            {
                return RedirectToAction("Login", "Account");
            }

            if (newPassword != confirmPassword) // Yeni şifreler eşleşmiyorsa
            {
                TempData["ErrorMessage"] = "Yeni şifreler eşleşmiyor!";
                return RedirectToAction("Index");
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword); // Şifreyi değiştir

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla güncellendi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Şifre güncellenirken bir hata oluştu!";
            }

            return RedirectToAction("Index");
        }
    }
}
