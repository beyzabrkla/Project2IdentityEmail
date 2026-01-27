using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutHeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
