using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;
using Project2IdentityEmail.Entities;
using System;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardMessageListViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EMailContext _context;

        public DashboardMessageListViewComponent(UserManager<AppUser> userManager, EMailContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var messages = await _context.UserMessages
                .Include(x => x.Sender)
                .Where(x => x.ReceiverId == user.Id && !x.IsTrash && !x.IsDraft)
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToListAsync();

            return View(messages);
        }
    }
}
