using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PresentationLayer.Controllers
{
    [Route("[controller]")]
    public class ChangePasswordController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ChangePasswordController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult Index(string token, string email)
        {

            var values = new ResetPasswordDto { Email = email, Token = token };
            return View();
        }


        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> Index(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.Password == resetPasswordDto.ConfirmPassword)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
                if (user != null)
                {
                    var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    if (!resetPassResult.Succeeded)
                    {
                        ViewBag.Error = "Şifre Değiştirilemedi";
                        return View(resetPasswordDto);
                    }
                    return RedirectToAction("Index", "Login");
                }
                ViewBag.Error = "Şifre Değiştirilemedi";
                return View(resetPasswordDto);
            }
            ViewBag.Error = "Şifreler uyuşmuyor";
            return View(resetPasswordDto);
        }


    }
}
