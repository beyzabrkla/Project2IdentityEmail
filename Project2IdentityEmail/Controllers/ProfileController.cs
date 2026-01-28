using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateProfile()
        {
            return RedirectToAction("Index");
        }
    }
}
