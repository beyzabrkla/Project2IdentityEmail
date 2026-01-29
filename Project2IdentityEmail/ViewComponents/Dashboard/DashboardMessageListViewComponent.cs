using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardMessageListViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
