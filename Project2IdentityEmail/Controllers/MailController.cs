using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.Controllers
{
    public class MailController : Controller
    {
        public IActionResult Inbox()
        {
            return View();
        }

        public IActionResult MailDetails()
        {
            return View();
        }

        public IActionResult ComposeMail(string to) //yeni mesaj
        {
            ViewBag.ReceiverEmail = to;
            return View();
        }

        public IActionResult ChatHistory(string email)
        {
            // Backend kısmında: select * from Messages where (Sender=@email or Receiver=@email) order by Date desc
            ViewBag.ChatPartner = email; // Sayfa başlığında kiminle konuştuğunu göstermek için
            return View();
        }
    }
}
