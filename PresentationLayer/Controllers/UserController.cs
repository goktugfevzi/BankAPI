using DTOLayer.DTOs.Auth;
using DTOLayer.DTOs.Card;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<User> _userManager;

        public UserController(IHttpClientFactory httpClientFactory, UserManager<User> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyAccount()
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> NameAction(string FirstName, string LastName, string Address)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            if (FirstName != null)
            {
                user.FirstName = FirstName;
            }
            if (LastName != null)
            {
                user.LastName = LastName;
            }
            if (Address != null)
            {
                user.Address = Address;
            }
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {

                return RedirectToAction("MyAccount", "User");

            }
            ViewBag.Error = "İsim Güncellenemedi";
            return RedirectToAction("MyAccount", "User");

        }
        [HttpPost]
        public async Task<IActionResult> EmailAction(string Email)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            if (Email != null)
            {
                user.Email = Email;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {

                return RedirectToAction("MyAccount", "User");

            }
            ViewBag.Error = "Email Adresi Güncellenemedi";
            return RedirectToAction("MyAccount", "User");

        }
        [HttpPost]
        public async Task<IActionResult> PhoneNumberAction(string Phone)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            if (Phone != null)
            {
                user.PhoneNumber = Phone;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {

                return RedirectToAction("MyAccount", "User");

            }
            ViewBag.Error = "Telefon numarası Güncellenemedi";
            return RedirectToAction("MyAccount", "User");

        }

        [HttpPost]
        public async Task<IActionResult> PasswordChangeAction(string oldPassword, string newPassword, string confirmPassword)
        {
            var UserID = HttpContext.Session.GetInt32("userid");

            if (newPassword == confirmPassword && newPassword != null && confirmPassword != null && oldPassword != null)
            {
                var changePasswordNewDto = new ChangePasswordNewDto
                {
                    Id = UserID.ToString(),
                    OldPassword = oldPassword,
                    NewPassword = newPassword,
                };
                var client = _httpClientFactory.CreateClient();
                var JsonData = JsonConvert.SerializeObject(changePasswordNewDto);
                StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7119/api/Authentication/ChangePasswordNew", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("MyAccount", "User");
                }
                ViewBag.Error = "Şifre Güncellenemiyor";
                return RedirectToAction("MyAccount", "User");

            }
            ViewBag.Error = "Şifreler Uyuşmuyor";
            return RedirectToAction("MyAccount", "User");

        }
        public async Task<IActionResult> MyCards()
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7119/api/Card/GetCardByAccount?id={user.Id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<ResultCardDto>>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MyCards(CreateCardDto createCardDto)
        {
            var UserID = HttpContext.Session.GetInt32("userid");
            var user = await _userManager.FindByIdAsync(UserID.ToString());
            createCardDto.Id = user.Id;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(createCardDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7119/api/Card", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyCards");
            }
            return View();
        }

        public async Task<IActionResult> DeleteCards(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7119/api/Card/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyCards");
            }
            return View();
        }
    }
}
