using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public LoginController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginUserDto loginUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(loginUserDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7119/api/Authentication/login", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("UserName", loginUserDto.UserName);
                var userm = await _userManager.FindByNameAsync(loginUserDto.UserName);
                HttpContext.Session.SetInt32("userid", userm.Id);
                //return RedirectToAction("Index", "Default");
                return RedirectToAction("Index", "Confirm");
            }
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
            return View();
        }
    }
}
