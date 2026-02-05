using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Models;

namespace Project2IdentityEmail.Controllers.Member
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ViewBag.Message = "Bu e-posta adresi sistemde kayıtlı değil.";
                return View();
            }

            // 1. Şifre Sıfırlama Token'ı Oluşturma
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "PasswordChange",
                new { userId = user.Id, token = token }, Request.Scheme);

            // 2. MimeKit ile Mail Gönderimi
            var mimeMessage = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Mendy Admin", "beyzailetisimapp@gmail.com");
            mimeMessage.From.Add(from);

            MailboxAddress to = new MailboxAddress("Kullanıcı", model.Email);
            mimeMessage.To.Add(to);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <div style='font-family:sans-serif; text-align:center;'>
                    <h2>Şifre Sıfırlama</h2>
                    <p>Şifrenizi yenilemek için aşağıdaki butona tıklayın:</p>
                    <a href='{resetLink}' style='background:#7460ee; color:white; padding:10px 20px; text-decoration:none; border-radius:5px;'>Şifremi Sıfırla</a>
                </div>";
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Şifre Değişiklik Talebi";

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("beyzailetisimapp@gmail.com", "qrmoackfzratkiky");
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
                ViewBag.Message = "Şifre yenileme linki başarıyla gönderildi.";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mail gönderilirken bir hata oluştu.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            // Verileri ResetPassword sayfasına taşımak için TempData kullanıyoruz
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];

            if (userId == null || token == null)
            {
                return RedirectToAction("ForgetPassword");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), model.Password);

            if (result.Succeeded)
            {
                // Başarılıysa Login sayfasına (Account/Login) yönlendir
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

    }
}
