using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Kişiye özel mail gönderme tetikleyicisi
        public IActionResult ComposeFromContact(string email)
        {
            // Direkt Mail/ComposeMail'e email bilgisini taşıyarak yönlendiriyoruz
            return RedirectToAction("ComposeMail", "Mail", new { to = email });
        }
    }
}
