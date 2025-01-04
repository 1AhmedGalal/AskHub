using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskHub.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Display(string id)
        {
            AppUser? appUser = _userManager.Users.FirstOrDefault(u => u.UserName == id);
            ViewBag.username = null;

            if (appUser != null)
            {
                ViewBag.username = id; // Pass the id to the view
            }

            return View();
        }

    }
}
