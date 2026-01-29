using Microsoft.AspNetCore.Mvc;

namespace Project2IdentityEmail.ViewComponents.Dashboard
{
    public class DashboardStatisticsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int totalMessages, int deletedMessages, int incomingMessages, int unreadMessages, int totalUsers)
        {
            ViewBag.TotalMessages = totalMessages;
            ViewBag.DeletedMessages = deletedMessages;
            ViewBag.IncomingMessages = incomingMessages;
            ViewBag.UnreadMessages = unreadMessages;
            ViewBag.TotalUsers = totalUsers;

            return View();
        }
    }
}