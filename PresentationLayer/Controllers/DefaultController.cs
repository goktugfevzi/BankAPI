using DTOLayer.DTOs.Account;
using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http;

namespace PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;


        public DefaultController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            @ViewBag.UserName=user.FirstName +" " +user.LastName;
            var client = _httpClientFactory.CreateClient();
            var responseMessage2 = await client.GetAsync($"https://localhost:7119/api/Transaction/TransactionListByAccountNumber?accountNumber={user.AccountNumber}");
            if (user != null)
            {

                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var value2 = JsonConvert.DeserializeObject<List<Transaction>>(jsonData2);
                var result = new UserTransactionDto
                {
                    transactions = value2,
                    user = user
                };
                return View(result);
            }
            return View();
        }
    }
}
