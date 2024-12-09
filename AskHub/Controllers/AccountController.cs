using AskHub.Models;
using AskHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskHub.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel userViewModel)   
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = userViewModel.Email,
                    Name = userViewModel.Name,
                    PasswordHash = userViewModel.Password,
                };

                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, userViewModel.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        if (error.Description.Contains("Username"))
                            error.Description = "Email Already Used";

                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(userViewModel.Email);
                if (user is not null)
                {
                    bool correctPassword = await _userManager.CheckPasswordAsync(user, userViewModel.Password);
                    
                    if (correctPassword)
                    {
                        await _signInManager.SignInAsync(user, userViewModel.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid Email or Password");
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
