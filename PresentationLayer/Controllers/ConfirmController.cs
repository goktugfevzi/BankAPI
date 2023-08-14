using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ConfirmController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ConfirmController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            Random random = new Random();
            int confirmCode = random.Next(100000, 1000000);

            var UserName = TempData["UserName"];
            var user = await _userManager.FindByNameAsync(UserName.ToString());
            if (user != null)
            {
                user.ConfirmCode = confirmCode;
                await _userManager.UpdateAsync(user);
                var value = new ConfirmCodeDto
                {
                    Id = user.Id,
                };
                return View(value);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfirmCodeDto confirmCodeDto)
        {
            var user = await _userManager.FindByIdAsync(confirmCodeDto.Id.ToString());
            string confirmCode = confirmCodeDto.Code1.ToString() + confirmCodeDto.Code2.ToString() + confirmCodeDto.Code3.ToString() + confirmCodeDto.Code4.ToString() + confirmCodeDto.Code5.ToString() + confirmCodeDto.Code6.ToString();

            if (user.ConfirmCode.ToString() == confirmCode)
            {
                TempData["UserId"] = user.Id;
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
