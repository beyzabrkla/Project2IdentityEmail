using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutSidebarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
