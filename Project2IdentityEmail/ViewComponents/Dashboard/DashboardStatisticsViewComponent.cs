using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardStatisticsViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EMailContext _context;

        public DashboardStatisticsViewComponent(UserManager<AppUser> userManager, EMailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userId = user.Id;

            // Giriş yapan kullanıcıya özel istatistikler
            ViewBag.TotalMessages = _context.UserMessages.Count(x => x.ReceiverId == userId || x.SenderId == userId);

            ViewBag.IncomingMessages = _context.UserMessages.Count(x => x.ReceiverId == userId && !x.IsTrash && !x.IsDraft);

            ViewBag.UnreadMessages = _context.UserMessages.Count(x => x.ReceiverId == userId && !x.IsRead && !x.IsTrash && !x.IsDraft);

            ViewBag.DeletedMessages = _context.UserMessages.Count(x => (x.ReceiverId == userId || x.SenderId == userId) && x.IsTrash);

            // Sistemdeki toplam kullanıcı ve kategori sayısı
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalCategory = _context.Categories.Count();

            return View();
        }
    }
}