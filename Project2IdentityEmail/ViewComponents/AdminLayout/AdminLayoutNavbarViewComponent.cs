using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutNavbarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); 
        }
    }
}
