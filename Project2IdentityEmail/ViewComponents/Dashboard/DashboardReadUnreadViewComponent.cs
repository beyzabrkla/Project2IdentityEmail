using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardReadUnreadViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EMailContext _context;

        public DashboardReadUnreadViewComponent(UserManager<AppUser> userManager, EMailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return View();

            var totalMessages = _context.UserMessages
                .Count(x => x.ReceiverId == user.Id && !x.IsDraft && !x.IsTrash);

            var readCount = _context.UserMessages
                .Count(x => x.ReceiverId == user.Id && x.IsRead && !x.IsDraft && !x.IsTrash);

            ViewBag.Read = readCount;
            ViewBag.Unread = totalMessages - readCount;

            return View();
        }
    }
}
