using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class MailLayoutSearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var searchTerm = HttpContext.Request.Query["searchTerm"].ToString();
            ViewBag.SearchTerm = searchTerm;

            return View();
        }
    }
}
