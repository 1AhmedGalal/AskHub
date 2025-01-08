using AskHub.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AskHub.Controllers
{
    public class UserFollowController : Controller
    {

        private readonly IUserFollowerRepository _userFollowRepository;

        public UserFollowController(IUserFollowerRepository userFollowRepository)
        {
            _userFollowRepository = userFollowRepository;
        }

        public IActionResult DisplayAllFollowings(string id)
        {
            List<string> result = _userFollowRepository.GetFollowingsUsernames(id);
            if(result.Any())
            {
                return View(result);
            }
            else
            { return View(null); }
        }

        public IActionResult DisplayAllFollowers(string id)
        {
            List<string> result = _userFollowRepository.GetFollowersUsernames(id);
            if (result.Any())
            {
                return View(result);
            }
            else
            { return View(null); }
        }

        public IActionResult Add(string id)
        {
            string[] usernames = id.Split('+');
            string follower = usernames[0];
            string following = usernames[1];

            _userFollowRepository.AddFollow(follower, following);
            return RedirectToAction("Display", "Profile", new { id = following });
        }

        public IActionResult Remove(string id)
        {
            string[] usernames = id.Split('+');
            string follower = usernames[0];
            string following = usernames[1];

            _userFollowRepository.RemoveFollow(follower, following);
            return RedirectToAction("Display", "Profile", new { id = following });
        }

        
    }
}
