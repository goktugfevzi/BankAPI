using DTOLayer.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(RegisterUserDto registerUserDto)
        {

            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(registerUserDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7119/api/Authentication/register", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "login");
            }

            return View();
        }

    }
}
