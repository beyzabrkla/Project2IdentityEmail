using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutNavbarViewComponent :ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminLayoutNavbarViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); // Oturumu açan kullanıcıyı al
            if (user ==null)
            {
                return View(new AppUser()); // Kullanıcı bulunamazsa boş bir AppUser nesnesi döndür
            }
            return View(user); 
        }
    }
}
