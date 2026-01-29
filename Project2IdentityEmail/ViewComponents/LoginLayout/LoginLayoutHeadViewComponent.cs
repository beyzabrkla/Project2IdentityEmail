using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.LoginLayout
{
    public class LoginLayoutHeadViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}