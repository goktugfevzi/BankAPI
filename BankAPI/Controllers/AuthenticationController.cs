using DTOLayer.DTOs.Auth;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Web;
using UserManagementService.Models;
using UserManagementService.Services;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public AuthenticationController(UserManager<User> userManager, IConfiguration configuration, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUser)
        {
            //Check User Exist 
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return BadRequest("Bu Mail bir kullanıcıya ait");
            }

            User user = new()
            {
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber,
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                ConfirmCode = 1,

                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.UserName,
            };

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            //var message = new Message(new string[] { user.Email! }, "Confirmation email link"!);
            //_emailService.SendEmail(message);

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginModel)
        {

            TestEmail(1026, DateTime.Now);
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                //var userRoles = await _userManager.GetRolesAsync(user);
                //foreach (var role in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, role));
                //}

                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
                //returning the token...

            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("confirmCode/{id}")]
        public async Task<IActionResult> ConfirmCode(int id)
        {
            Random random = new Random();
            int confirmCode = random.Next(100000, 1000000);
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.ConfirmCode = confirmCode;
                await _userManager.UpdateAsync(user);
                return Ok("ConfirmCode oluşturuldu ve yönlendiriliyor...");
            }
            return BadRequest($"{id} ID ye sahip üye yok!");
        }
        [HttpPost]


        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password/{email}")]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var forgotPasswordLink = Url.Action(nameof(ResetPassword), "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot Password Link", forgotPasswordLink!);
                _emailService.SendEmail(message);
                return Ok($"Password Change Request is sent on Email {user.Email}. Please Open your meail & Click the link.");
            }
            return BadRequest("Couldnot send link to email, please try again");
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user != null)
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
                return Ok("Password has been changed");
            }
            return BadRequest("Couldnot send link to email, please try again");
        }


        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return Ok(new { model });
        }

        [HttpGet]
        public IActionResult TestEmail(int amount, DateTime time)
        {

            Random random = new Random();
            int confirmCode = random.Next(100000, 1000000);
            var message = new Message(new string[] { "goktugfevziozcelik@gmail.com" }, "PARA GONDERME ISLEMI", $"SIFRENIZI PAYLASMAYINIZ. {time} TARIHLI {amount} MIKTARINDA PARAYI GONDERMEK ICIN 3D SECURE SIFRENIZ: {confirmCode}. GUVENLIGINIZ ICIN BU SIFREYI BANKA PERSONELI DAHIL KIMSEYLE PAYLASMAYIN. KEYIFLI GUNLER DILERIZ..");
            _emailService.SendEmail(message);
            return Ok("Mail gönderildi");
        }

        //[HttpGet("ConfirmEmail")]
        //public async Task<IActionResult> ConfirmEmail(string token, string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var result = await _userManager.ConfirmEmailAsync(user, token);
        //        if (result.Succeeded)
        //        {
        //            return Ok("Email Verified Successfully");

        //        }
        //    }
        //    return BadRequest("This User Doesnot exist!");
        //}


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

    }
}
