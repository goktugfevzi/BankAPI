using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly UserManager<User> _userManager;

        public DefaultController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var UserName = TempData["UserName"];
            var user = await _userManager.FindByNameAsync(UserName.ToString());
            return View(user);
        }
    }
}
