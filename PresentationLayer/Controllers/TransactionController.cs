using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DepositMoney()
        {
            return View();
        }

        public IActionResult SendMoney()
        {
            return View();
        }
    }
}
