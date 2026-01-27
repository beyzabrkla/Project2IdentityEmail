using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult Sendbox()
        {
            return View();
        }

        public IActionResult DraftsMessage()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Spam()
        {
            return View();
        }

        public IActionResult Trash()
        {
            return View();
        }

    }
}
