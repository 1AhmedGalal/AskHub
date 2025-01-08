using AskHub.Models;
using AskHub.Repositories;
using AskHub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AskHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userManager, IQuestionRepository questionRepository, IUserFollowerRepository userFollowerRepository, ILogger<HomeController> logger)
        {
            _questionRepository = questionRepository;
            _userFollowerRepository = userFollowerRepository;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity == null)
            {
                return View(null);
            }

            QuestionDirection direction = QuestionDirection.DESTINATION;

            string loggedInUsername = User.Identity.Name!;
            var loggedInUser = await _userManager.FindByNameAsync(loggedInUsername);
            string loggedInUserId = loggedInUser.Id;

            List<string> followings = _userFollowerRepository.GetFollowingsUsernames(loggedInUsername);
            List<Question>? questions = new List<Question>();

            foreach(string username in followings)
            {
                var user = await _userManager.FindByNameAsync(username);
                string userId = user.Id;
                var tmp = _questionRepository.GetAllByUserId(userId, direction);

                if(tmp != null)
                    questions.AddRange(tmp);
            }

            List<QuestionViewModel> displayedQuestions = new List<QuestionViewModel>();
            string? sourceUsername = null;
            string? destinationUsername = null;

            foreach (Question question in questions)
            {
                if (!question.Seen)
                    continue;

                if (question.SourceAppUser is null)
                    sourceUsername = "None";
                else
                    sourceUsername = question.SourceAppUser.UserName;

                destinationUsername = question.DestinationAppUser!.UserName ?? "None";

                QuestionViewModel askedQuestion = new QuestionViewModel()
                {
                    Id = question.Id,
                    Content = question.Content,
                    SourceUsername = sourceUsername,
                    DestinationUsername = question.DestinationAppUser!.UserName,
                    DateTime = question.CreatedDate,
                    Seen = question.Seen
                };
                displayedQuestions.Add(askedQuestion);
            }

            if (displayedQuestions.Count == 0)
            {
                return View(null);
            }
            else
            {
                return View(displayedQuestions);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}