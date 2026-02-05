using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardProfileViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardProfileViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); // Oturum açmış kullanıcıyı alır

            if (user == null)
            {
                return Content("Kullanıcı bulunamadı");
            }

            return View(user);
        }
    }
}

