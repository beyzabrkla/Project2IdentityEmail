using MailKit.Net.Smtp;
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
            // 1. Validasyon Kontrolü
            if (createUserRegisterDTO.Password != createUserRegisterDTO.ConfirmPassword)
            {
                return Json(new { success = false, message = "Şifreler uyuşmuyor." });
            }

            if (createUserRegisterDTO.ImageFile == null || createUserRegisterDTO.ImageFile.Length == 0)
            {
                return Json(new { success = false, message = "Lütfen bir profil fotoğrafı seçiniz." });
            }

            // 2. 6 Haneli Onay Kodu Üretme
            Random random = new Random();
            string code = random.Next(100000, 999999).ToString();

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(createUserRegisterDTO.ImageFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImages/", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await createUserRegisterDTO.ImageFile.CopyToAsync(stream);
            }

            // 3. AppUser Nesnesi Oluşturma
            var appUser = new AppUser()
            {
                Name = createUserRegisterDTO.Name,
                Surname = createUserRegisterDTO.Surname,
                Email = createUserRegisterDTO.Email,
                UserName = createUserRegisterDTO.Username,
                ImageUrl = fileName,
                ConfirmCode = code,
                ConfirmDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(appUser, createUserRegisterDTO.Password);

            if (result.Succeeded)
            {
                try
                {
                    // 4. Mail Gönderimi
                    MimeMessage mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Mendy Admin", "beyzailetisimapp@gmail.com"));
                    mimeMessage.To.Add(new MailboxAddress("Sayın Kullanıcı", appUser.Email));
                    mimeMessage.Subject = "E-Posta Onay Kodu";

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini tamamlamak için 6 haneli onay kodunuz: " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, false);
                        await client.AuthenticateAsync("beyzailetisimapp@gmail.com", "qrmoackfzratkiky");
                        await client.SendAsync(mimeMessage);
                        await client.DisconnectAsync(true);
                    }

                    return Json(new { success = true, email = appUser.Email });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Mail gönderilirken hata oluştu: " + ex.Message });
                }
            }

            // Eğer result.Succeeded başarısız olursa (Email zaten kayıtlıysa)
            var errors = string.Join("<br>", result.Errors.Select(x => x.Description));
            return Json(new { success = false, message = errors });
        }

        //Kullanıcının modal içine yazdığı kodu kontrol edicek
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && user.ConfirmCode == code)
            {
                user.EmailConfirmed = true;
                user.TwoFactorEnabled = true; // Case'in istediği 2FA aktifleştirme

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, message = "Onay kodu hatalı veya süresi dolmuş." });
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            // kullanıcıyı bul, eğer varsa şifresini kontrol et, eğer başarılıysa kullanıcıyı sisteme dahil et
            var user = await _userManager.FindByNameAsync(loginUserDTO.Username);

            if (user != null)
            {
                // 1. Kullanıcı şifresini kontrol et (Oturum açmadan sadece doğrulama yap)
                var result = await _signInManager.PasswordSignInAsync(loginUserDTO.Username, loginUserDTO.Password, loginUserDTO.IsPersistent, false);

                // 2. Eğer başarılıysa VEYA 2FA bekliyorsa (biz zaten kayıt anında onayladığımız için içeri alıyoruz)
                if (result.Succeeded || result.RequiresTwoFactor)
                {
                    // Kullanıcıyı manuel olarak sisteme dahil et
                    await _signInManager.SignInAsync(user, isPersistent: loginUserDTO.IsPersistent);

                    return RedirectToAction("Index", "Dashboard");
                }

                // Şifre yanlışsa buraya düşer
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
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