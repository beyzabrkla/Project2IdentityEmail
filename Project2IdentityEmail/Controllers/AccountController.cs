using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Backend bağlandığında burada kimlik doğrulaması yapılacak
            // if (user != null) { ... }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(object userModel) // Model sonra netleşecek
        {
            // Kullanıcı kayıt işlemleri buraya gelecek
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            // Session veya Cookie temizleme
            return RedirectToAction("Login");
        }
    }
}
