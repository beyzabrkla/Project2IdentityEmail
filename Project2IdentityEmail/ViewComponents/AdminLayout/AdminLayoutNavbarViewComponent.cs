using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.ViewComponents.AdminLayout
{
    public class AdminLayoutNavbarViewComponent :ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EMailContext _context;

        public AdminLayoutNavbarViewComponent(UserManager<AppUser> userManager, EMailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return View(new AppUser());

            var unreadMessages = _context.UserMessages
                .Include(x => x.Sender)
                .Where(x => x.ReceiverId == user.Id &&
                            x.IsRead == false &&
                            x.IsTrash == false &&
                            x.IsDraft == false)
                .OrderByDescending(x => x.Date)
                .Select(x => new {
                    x.Id,
                    SenderEmail = x.Sender.Email,
                    Subject = x.Subject,
                    SendTime = x.Date.ToString("HH:mm")
                }).ToList();

            ViewBag.MessageCount = unreadMessages.Count;
            ViewBag.LastMessages = unreadMessages;

            return View(user);
        }
    }
}
