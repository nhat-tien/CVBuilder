using CVBuilder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Controllers
{
    public class AccountController : Controller
    {

        public record LoginRequest(string Email, string Password);
        public record RegisterRequest(string Name, string Email, string Password);

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginRequest req)
        {
            if (!ModelState.IsValid)
                return View(req);

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(req);
            }

            var result = await _signInManager.PasswordSignInAsync(user, req.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is incorrect");
                return View(req);
            }

            return await RedirectUserToDashboard(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register([Bind("Name,Email,Password")] RegisterRequest req)
        {
            if (!ModelState.IsValid)
                return View(req);

            var exists = await _userManager.FindByEmailAsync(req.Email);
            if (exists != null)
            {
                ModelState.AddModelError("Email", "Email is already registered");
                return View(req);
            }

            var user = new User
            {
                UserName = req.Email,
                Email = req.Email,
                Name = req.Name
            };

            var result = await _userManager.CreateAsync(user, req.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(req);
            }

            await _userManager.AddToRoleAsync(user, "User");

            await _signInManager.SignInAsync(user, isPersistent: true);

            return await RedirectUserToDashboard(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RedirectUserToDashboard(User user)
        {

            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            switch(role)
            {
                case "Admin": 
                    return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
                case "User": 
                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
    }
}
