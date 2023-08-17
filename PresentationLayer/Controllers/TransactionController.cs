using DTOLayer.DTOs.Card;
using DTOLayer.DTOs.Operations;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public TransactionController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DepositMoney()
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            @ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DepositMoney(DepositDto depositDto)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            user.Balance += depositDto.Amount;
            await _userManager.UpdateAsync(user);
            depositDto.SenderAccountNumber = user.AccountNumber;
            depositDto.TransactionDate = DateTime.Now;
            depositDto.TransactionTypeID = 1;           
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(depositDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7119/api/Operations/Deposit", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { SONUC = true });
            }
            return View();
        }

        public async Task<IActionResult> SendMoney()
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            @ViewBag.UserName = user.FirstName + " " + user.LastName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMoney(SendMoneyDto sendMoneyDto)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            user.Balance -= sendMoneyDto.Amount;
            await _userManager.UpdateAsync(user);
            sendMoneyDto.SenderAccountNumber = user.AccountNumber;
            sendMoneyDto.ReceiverAccountNumber = sendMoneyDto.ReceiverAccountNumber;
            sendMoneyDto.TransactionDate = DateTime.Now;
            sendMoneyDto.TransactionTypeID = 2;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(sendMoneyDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7119/api/Operations/SendMoney", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { SONUC = true });
            }
            return View();
        }
    }
}
