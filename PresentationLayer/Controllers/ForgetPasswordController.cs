using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ForgetPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
