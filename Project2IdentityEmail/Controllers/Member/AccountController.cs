using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Project2IdentityEmail.DTOs;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Controllers.Member
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRegisterDTO createUserRegisterDTO)
        {
            if (createUserRegisterDTO.Password != createUserRegisterDTO.ConfirmPassword)
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor.");
                return View(createUserRegisterDTO);
            }

            // Resim Zorunluluğu Kontrolü
            if (createUserRegisterDTO.ImageFile == null || createUserRegisterDTO.ImageFile.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir profil fotoğrafı seçiniz.");
                return View(createUserRegisterDTO);
            }

            // Dosyayı Sunucuya Kaydetme İşlemi
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(createUserRegisterDTO.ImageFile.FileName); // Benzersiz dosya adı oluşturma
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages/", fileName); // Dosyanın kaydedileceği tam yol

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await createUserRegisterDTO.ImageFile.CopyToAsync(stream);
            }

            // 4. Kullanıcı Nesnesini Hazırlama
            var appUser = new AppUser()
            {
                Name = createUserRegisterDTO.Name,
                Surname = createUserRegisterDTO.Surname,
                Email = createUserRegisterDTO.Email,
                UserName = createUserRegisterDTO.Username,
                ImageUrl = fileName, // Kaydettiğimiz dosya adını DB'ye yazıyoruz
                ConfirmDate = DateTime.Now
            };

            // Identity ile Kayıt
            var result = await _userManager.CreateAsync(appUser, createUserRegisterDTO.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Hesabınız başarıyla oluşturuldu. Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }

            // Identity'den gelen hataları ekrana bas
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(createUserRegisterDTO);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Username, loginUserDTO.Password, loginUserDTO.IsPersistent, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}