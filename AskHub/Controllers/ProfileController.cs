using Microsoft.AspNetCore.Mvc;

namespace AskHub.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Display(string id)
        {
            ViewBag.username = id; // Pass the id to the view
            return View();
        }
    }
}
