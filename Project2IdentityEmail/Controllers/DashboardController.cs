using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.TotalMessages = 5600;
            ViewBag.DeletedMessages = 345;
            ViewBag.IncomingMessages = 1250;
            ViewBag.UnreadMessages = 18;

            ViewBag.TotalUsers = _userManager.Users.Count();

            return View();
        }
    }
}