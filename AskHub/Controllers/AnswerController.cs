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
    public class AnswerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;

        public AnswerController(IQuestionRepository questionRepository, IAnswerRepository answerRepository, UserManager<AppUser> userManager)
        {
            _questionRepository = questionRepository;
            _userManager = userManager;
            _answerRepository = answerRepository;
        }

        public IActionResult Display(int id)
        {
            Answer? answer = _answerRepository.GetByQuestionId(id);

            if (answer is null)
            {
                throw new Exception("Data Doesn't Exist");
            }

            AnswerViewModel answerView = new AnswerViewModel() { Content = answer.Content };
            return View(answerView);
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int id, AnswerViewModel? answerViewModel = null)
        {
            ViewBag.Succeeded = null;
            Question? question = _questionRepository.GetByQuestionId(id);

            if (question == null)
            {
                throw new ArgumentException("Question Not Found");
            }

            if (answerViewModel != null && ModelState.IsValid)
            {
                Answer answer = new Answer()
                {
                    Content = answerViewModel.Content,
                    QuestionId = id,
                    Question = question
                    //CreatedDate = DateTime.Now
                };

                _answerRepository.Add(answer);
                _questionRepository.MarkAsSeen(question);

                ViewBag.Succeeded = "success";
            }

            return View(answerViewModel);
        }
    }
}
