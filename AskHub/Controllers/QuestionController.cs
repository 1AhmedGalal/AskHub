using AskHub.Models;
using AskHub.Repositories;
using AskHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult DisplayAll(string userId, QuestionDirection direction)
        {
            List<Question>? questions = _questionRepository.GetAllByUserId(userId, direction);

            if(questions is null)
            {
                ViewBag.Empty = true;
                return View();
            }

            List<QuestionViewModel> displayedQuestions = new List<QuestionViewModel>();
            string? sourceUsername = null;
            string? destinationUsername = null;

            foreach (Question question in questions)
            {
                if (direction == QuestionDirection.SOURCE)
                {
                    AppUser user = _userManager.Users.FirstOrDefault(u => u.Id == question.DestinationAppUserId)!;
                    destinationUsername = user.UserName;
                }
                else
                {
                    AppUser user = _userManager.Users.FirstOrDefault(u => u.Id == question.DestinationAppUserId)!;
                    sourceUsername = user.UserName;
                }

                QuestionViewModel askedQuestion = new QuestionViewModel()
                { 
                    Id = question.Id,
                    Content = question.Content,
                    SourceUsername = sourceUsername,
                    DestinationUsername = destinationUsername,
                    DateTime = question.CreatedDate
                };

                displayedQuestions.Add(askedQuestion);
            }

            return View(displayedQuestions);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AskQuestionViewModel questionViewModel, AppUser sourceUser, AppUser destinationUser)
        {
            ViewBag.Succeeded = false;

            if (ModelState.IsValid)
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
                ViewBag.Succeeded = true;
            }

            return Add(questionViewModel, sourceUser, destinationUser);
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdateQuestionViewModel questionViewModel)
        {
            /*
             may need to add the old text first by myself!!!!!!!!!!!!!!!!!!!!
             */

            ViewBag.Succeded = false;
            Question question = _questionRepository.GetByQuestionId(questionViewModel.Id)!;
            
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
                    ViewBag.Succeded = true;
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
            ViewBag.Succeeded = true;
            try
            {
                _questionRepository.Delete(id);
            }
            catch
            {
                ViewBag.Succeeded = false;
            }

            return View();
        }

    }
}
