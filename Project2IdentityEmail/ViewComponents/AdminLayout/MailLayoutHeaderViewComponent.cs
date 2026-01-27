using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class MailLayoutHeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
