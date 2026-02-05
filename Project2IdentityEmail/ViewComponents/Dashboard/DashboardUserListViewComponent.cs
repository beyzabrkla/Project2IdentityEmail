using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardUserListViewComponent: ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardUserListViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Giriş yapan kullanıcıyı al
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            // Giriş yapan kullanıcı hariç diğer kullanıcıları al
            var values = _userManager.Users
                .Where(u => u.Id != currentUser.Id) // Mevcut kullanıcıyı hariç tut
                .ToList();

            return View(values);
        }
    }
}
