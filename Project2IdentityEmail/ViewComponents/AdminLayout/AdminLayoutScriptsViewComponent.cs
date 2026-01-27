using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutScriptsViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
