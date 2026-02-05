using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.LoginLayout
{
    public class LoginLayoutScriptsViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
