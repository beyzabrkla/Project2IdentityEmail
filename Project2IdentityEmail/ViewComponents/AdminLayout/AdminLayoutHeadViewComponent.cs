using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutHeadViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
