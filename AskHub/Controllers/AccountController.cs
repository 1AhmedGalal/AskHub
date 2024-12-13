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
                    UserName = userViewModel.Username,
                    Email = userViewModel.Email,
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
                AppUser user = await _userManager.FindByEmailAsync(userViewModel.Email);
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

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateViewModel userViewModel)
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)!;
                var result = await _userManager.DeleteAsync(user);
                
                if (result.Succeeded)
                {
                    user.PasswordHash = userViewModel.Password;
                    result = await _userManager.CreateAsync(user, userViewModel.Password);

                    if (result.Succeeded)
                    {
                        ViewBag.Message = "Updated Password Successfully";
                    }
                    else
                    {
                        ViewBag.Message = null;
                    }
                }
                
                return View(userViewModel);
            }
            else
            {
                return NotFound("User not found.");
            }
        }



        public async Task<IActionResult> Delete()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)!;
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return NotFound("User not found.");
            }
        }
    }
}
