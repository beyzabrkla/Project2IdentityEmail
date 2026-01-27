using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project2IdentityEmail.DTOs;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRegisterDTO createUserRegisterDTO)
        {
            AppUser appUser = new AppUser()
            {
                Name = createUserRegisterDTO.Name,
                Email = createUserRegisterDTO.Email,
                Surname = createUserRegisterDTO.Surname,
                UserName = createUserRegisterDTO.Username
            };
            await _userManager.CreateAsync(appUser, createUserRegisterDTO.Password);
            return RedirectToAction("UserList");
        }
    }
}
