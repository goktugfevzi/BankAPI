using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        public IActionResult MyAccount()
        {
            return View();
        }

        public IActionResult MyCards()
        {
            return View();
        }
    }
}
