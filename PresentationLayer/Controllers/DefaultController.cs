using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
