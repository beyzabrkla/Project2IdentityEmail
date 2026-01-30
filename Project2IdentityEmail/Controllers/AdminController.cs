using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Models;

namespace Project2IdentityEmail.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // 1. Tüm Kullanıcıları Listeleme
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewBag.UserName = user.UserName;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleAssignViewModels = new List<RoleAssignViewModel>();
            foreach (var item in roles)
            {
                roleAssignViewModels.Add(new RoleAssignViewModel
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                    IsExist = userRoles.Contains(item.Name)
                });
            }
            return View(roleAssignViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            foreach (var item in model)
            {
                if (item.IsExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserList");
        }
    }
}
