using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DTOLayer.DTOs.Account;
using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class BillController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public BillController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            ViewBag.User = user;


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7119/api/Bill");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Bill>>(jsonData);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int BillID)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            var payBillDto = new PayBillDto
            {
                UserID = user.Id,
                BillId = BillID
            };
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(payBillDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"https://localhost:7119/api/Operations/PayBill", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { SONUC = true });
            }
            ViewBag.Error = responseMessage.RequestMessage;
            return View();

        }
    }
}
