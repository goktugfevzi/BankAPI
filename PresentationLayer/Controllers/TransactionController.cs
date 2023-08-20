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
            sendMoneyDto.SenderAccountNumber = user.AccountNumber;
            sendMoneyDto.TransactionDate = DateTime.Now;
            sendMoneyDto.TransactionTypeID = 2;            
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(sendMoneyDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7119/api/Operations/CheckSendMoney", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var values = await responseMessage.Content.ReadAsStringAsync();                
                if (values == "Yetersiz Bakiye")
                {
                    return Json(new { uygun = false });
                }
                else if (values == "Hesap Bulunamadı")
                {
                    return Json(new { sonuc = false });
                }
                else if (values == "Başarılı İşlem")
                {
                    var client3 = _httpClientFactory.CreateClient();
                    var responseMessage3 = await client.GetAsync($"https://localhost:7119/api/Transaction/TransactionByAccountNumber?accountNumber={sendMoneyDto.ReceiverAccountNumber}");
                    if (responseMessage3.IsSuccessStatusCode)
                    {
                        var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                        var value3 = JsonConvert.DeserializeObject<User>(jsonData3);
                        var userreceiver = await _userManager.FindByIdAsync(value3.Id.ToString());
                        userreceiver.Balance += sendMoneyDto.Amount;
                        await _userManager.UpdateAsync(userreceiver);
                    }
                    
                    user.Balance -= sendMoneyDto.Amount;
                    await _userManager.UpdateAsync(user);
                    var JsonData2 = JsonConvert.SerializeObject(sendMoneyDto);
                    StringContent content2 = new StringContent(JsonData2, Encoding.UTF8, "application/json");
                    var responseMessage2 = await client.PostAsync("https://localhost:7119/api/Operations/SendMoney", content2);

                    if (responseMessage2.IsSuccessStatusCode)
                    {
                        return Json(new { sonuc = true });
                    }
                    return View();
                }
                else
                {
                    return Json(new { uygun = false });
                }
            }
            else
            {
                return View();
            }


        }
    }
}
