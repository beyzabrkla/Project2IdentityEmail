using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Context;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardCategoryListViewComponent :ViewComponent
    {
        private readonly EMailContext _context;

        public DashboardCategoryListViewComponent(EMailContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Kategorileri ve her kategoriye ait mesaj sayısını çekiyoruz
            var categoryData = await _context.Categories
                .Select(c => new
                {
                    CategoryName = c.CategoryName,
                    // Sadece taslak olmayan ve silinmemiş mesajları sayıyoruz
                    MessageCount = _context.UserMessages.Count(m => m.CategoryId == c.Id && !m.IsDraft && !m.IsTrash)
                }).ToListAsync();

            // Chart.js'in anlayacağı formatta diziye çeviriyoruz
            ViewBag.CategoryLabels = categoryData.Select(x => x.CategoryName).ToArray();
            ViewBag.CategoryCounts = categoryData.Select(x => x.MessageCount).ToArray();

            return View();
        }
    }
}
