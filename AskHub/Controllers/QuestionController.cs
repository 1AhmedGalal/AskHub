using AskHub.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AskHub.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        public IActionResult DisplayAll()
        {
            //get dest user from a session!

            return Content("77");
        }

        public IActionResult Add(QuestionViewModel questionViewModel)
        {
            //get src user from userMgr
            //get dest user from a session!
            //make sure that src != dest [but add that in repo ((maybe))]
            return Content("77");
        }

        public IActionResult Update(QuestionViewModel questionViewModel, DateTime dateCreated)
        {
            //get src user from userMgr
            //get dest user from a session!
            //make sure that src != dest [but add that in repo ((maybe))]

            return Content("77");
        }

        public IActionResult Delete(DateTime dateCreated)
        {
            //get src user from userMgr
            //get dest user from a session!
            //make sure that src != dest [but add that in repo ((maybe))]

            return Content("77");
        }

    }
}
