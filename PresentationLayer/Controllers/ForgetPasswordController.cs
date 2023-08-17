using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class ForgetPasswordController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public ForgetPasswordController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            @ViewBag.UserName = user.FirstName + " " + user.LastName;
            if (user != null)
            {
                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(Email);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                await client.PostAsync($"https://localhost:7119/api/Authentication/forgot-password/{Email}", content);
                ViewBag.Response = "Mailinize Kod Gönderilmiştir";
                return View();
            }
            ViewBag.Response = "Böyle bir mail bulunamamaktadır";
            return View();
        }
    }
}
