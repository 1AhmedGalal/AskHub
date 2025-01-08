using AskHub.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AskHub.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserFollowerRepository _userFollowRepository;

        public ProfileController(IUserFollowerRepository userFollowRepository, UserManager<AppUser> userManager)
        {
            _userFollowRepository = userFollowRepository;
            _userManager = userManager;
        }

        public IActionResult Display(string id)
        {
            AppUser? profileUser = _userManager.Users.FirstOrDefault(u => u.UserName == id);
            string loggedInUsername = User.Identity!.Name!;

            ViewBag.username = null;

            if (profileUser != null)
            {
                ViewBag.username = id; // Pass the id to the view
                ViewBag.IsFollowing = _userFollowRepository.IsFollowing(loggedInUsername, profileUser.UserName);
                ViewBag.FollowersCount = _userFollowRepository.CountFollowers(profileUser.UserName);
            }

            return View();
        }

    }
}
