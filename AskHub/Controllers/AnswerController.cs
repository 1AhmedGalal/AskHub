using AskHub.Repositories;
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

        public AnswerController(IQuestionRepository questionRepository, UserManager<AppUser> userManager)
        {
            _questionRepository = questionRepository;
            _userManager = userManager;
        }

        //public I
    }
}
