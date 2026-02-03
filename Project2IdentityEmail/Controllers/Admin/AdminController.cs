using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.Models;

namespace Project2IdentityEmail.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> FastAssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Json(new { success = false, message = "Kullanıcı bulunamadı." });

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                return Json(new { success = true, message = "Yetki başarıyla güncellendi." });

            return Json(new { success = false, message = "Bir hata oluştu." });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdminRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Json(new { success = false, message = "Kullanıcı bulunamadı." });

            // Admin rolünü kaldır
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                // GÜVENLİK KONTROLÜ: Eğer adminliği alınan kişi, şu an sisteme giriş yapmış olan kişiyse
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null && currentUser.Id == userId)
                {
                    // Kullanıcıyı oturumdan at (SignOut)
                    await _signInManager.SignOutAsync();
                    return Json(new { success = true, isSelfDelete = true, message = "Kendi yetkinizi aldınız. Giriş sayfasına yönlendiriliyorsunuz." });
                }

                return Json(new { success = true, isSelfDelete = false, message = "Admin yetkisi başarıyla geri alındı." });
            }
            return Json(new { success = false, message = "İşlem başarısız." });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return Json(new { success = false, message = "Rol ismi boş olamaz." });

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new AppRole { Name = roleName });
                if (result.Succeeded) return Json(new { success = true, message = "Yeni rol başarıyla sisteme eklendi." });
            }
            return Json(new { success = false, message = "Rol zaten mevcut veya hata oluştu." });
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            ViewBag.UserName = $"{user.Name} {user.Surname}";
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = roles.Select(item => new RoleAssignViewModel
            {
                RoleId = item.Id,
                RoleName = item.Name,
                IsExist = userRoles.Contains(item.Name)
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            foreach (var item in model)
            {
                if (item.IsExist)
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                else
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}