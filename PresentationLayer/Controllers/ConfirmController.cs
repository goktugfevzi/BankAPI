using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using UserManagementService.Models;
using UserManagementService.Services;

namespace PresentationLayer.Controllers
{
    public class ConfirmController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;


        public ConfirmController(UserManager<User> userManager, IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _httpClientFactory = httpClientFactory;
        }


        public async Task<ActionResult> Index()
        {
            Random random = new Random();
            int confirmCode = random.Next(100000, 1000000);

            var UserName = HttpContext.Session.GetString("UserName");
            var user = await _userManager.FindByNameAsync(UserName.ToString());
            @ViewBag.UserName = user.FirstName + " " + user.LastName;
            if (user != null)
            {
                user.ConfirmCode = confirmCode;
                await _userManager.UpdateAsync(user);
                var value = new ConfirmCodeDto
                {
                    Id = user.Id,
                };
                TestEmail(confirmCode, DateTime.Now, user.Email);
                return View(value);
            }
            return RedirectToAction("Index", "Login");
        }

        private async void TestEmail(int amount, DateTime time, string email)
        {
            var sendMail = new SendMailDto
            {
                email = email,
                subject = "DOGRULAMA KODU",
                text = $"DOGRULAMA KODUNUZU PAYLASMAYINIZ. {time} TARIHLI BANKAYA GIRIS YAPMAK ICIN DOGRULAMA KODUNUZ: {amount}. GUVENLIGINIZ ICIN BU KODU BANKA PERSONELI DAHIL KIMSEYLE PAYLASMAYIN. KEYIFLI GUNLER DILERIZ.."
            };
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(sendMail);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7119/api/Authentication/EmailSend", content);

        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmCodeDto confirmCodeDto)
        {
            var user = await _userManager.FindByIdAsync(confirmCodeDto.Id.ToString());
            string confirmCode = confirmCodeDto.Code1.ToString() + confirmCodeDto.Code2.ToString() + confirmCodeDto.Code3.ToString() + confirmCodeDto.Code4.ToString() + confirmCodeDto.Code5.ToString() + confirmCodeDto.Code6.ToString();
            @ViewBag.UserName = user.FirstName + " " + user.LastName;
            if (user.ConfirmCode.ToString() == confirmCode)
            {                
                HttpContext.Session.SetInt32("userid", user.Id);
                return RedirectToAction("Index", "Default");
            }
            else
            {
                ViewBag.Error = "Hatalı Giriş";
                var value = new ConfirmCodeDto
                {
                    Id = user.Id,
                };
                return View(value);
            }
        }

    }




}
