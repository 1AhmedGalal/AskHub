using AskHub.Models;
using AskHub.Repositories;
using AskHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AskHub.Controllers
{
    /*
     * I ALSO NEED TO LIMIT ACCES SO IT IS CALLED FROM PROFILE ONLY!!!!!!!!
     */

    [Authorize]
    public class QuestionController : Controller
    {
       
        private readonly UserManager<AppUser> _userManager;
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository, UserManager<AppUser> userManager) 
        { 
            _questionRepository = questionRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> DisplayAll(string id)
        {
            QuestionDirection direction = QuestionDirection.DESTINATION;

            string dir = id.Substring(1, 3);
            if (dir == "dst")
            {
                direction = QuestionDirection.DESTINATION;
            }
            else if(dir == "src")
            {
                direction = QuestionDirection.SOURCE;
            }
            else
            {
                throw new Exception("Invalid Choice");
            }

            string username = id.Substring(4, id.Length - 4);
            var givenUser = await _userManager.FindByNameAsync(username);
            string userId = givenUser.Id;

            List<Question>? questions = _questionRepository.GetAllByUserId(userId, direction);

            if(questions is null)
            {
                ViewBag.Empty = true;
                return View(null);
            }

            List<QuestionViewModel> displayedQuestions = new List<QuestionViewModel>();
            string? sourceUsername = null;
            string? destinationUsername = null;
            
            foreach (Question question in questions)
            {
                if (id.Substring(0, 1) == "0" && !question.Seen)
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

            return View(displayedQuestions);
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string id, AskQuestionViewModel? questionViewModel = null)
        {
            string destinationUsername = id;
            string sourceUsername = User.Identity!.Name!; 
            var sourceUser = await _userManager.FindByNameAsync(sourceUsername);
            var destinationUser = await _userManager.FindByNameAsync(destinationUsername);

            if(sourceUser == null || destinationUser == null)
            {
                throw new Exception("User Doesn't Exist");
            }
            else if(sourceUser == destinationUser)
            {
                throw new Exception("Can't Send Message To Yourself");
            }

            ViewBag.Succeeded = null;

            if (questionViewModel != null && ModelState.IsValid)
            {
                Question question = new Question()
                {
                    Content = questionViewModel.Content,
                    CreatedDate = DateTime.Now,
                    Seen = false,
                    DestinationAppUserId = destinationUser.Id,
                    DestinationAppUser = destinationUser,
                    SourceAppUserId = (questionViewModel.AskAnonymously) ? null : sourceUser.Id,
                    SourceAppUser = (questionViewModel.AskAnonymously) ? null : sourceUser
                };

                _questionRepository.Add(question);
                ViewBag.Succeeded = "success";
            }

            return View(questionViewModel);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(int id, UpdateQuestionViewModel questionViewModel)
        {
            /*
             may need to add the old text first by myself!!!!!!!!!!!!!!!!!!!!
             */

            ViewBag.Succeeded = null;
            Question question = _questionRepository.GetByQuestionId(id)!;

            if (question is null || question.Seen)
            {
                return NotFound("Couldn't Edit");
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    question.Content = questionViewModel.Content;
                    _questionRepository.Update(question);
                    ViewBag.Succeeded = "success";
                }
                catch
                {
                    ModelState.AddModelError("", "Couldn't Edit");
                }
            }

            return View(questionViewModel);
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Succeeded = null;

            try
            {
                _questionRepository.Delete(id);
                ViewBag.Succeeded = "success";
            }
            catch
            {
                ViewBag.Succeeded = null;
            }

            return View();
        }

    }
}
