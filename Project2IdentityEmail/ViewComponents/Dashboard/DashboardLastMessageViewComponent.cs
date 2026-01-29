using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardLastMessageViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
