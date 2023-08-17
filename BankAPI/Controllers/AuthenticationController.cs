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
            Random rnd = new Random();
            int number = rnd.Next(40000000, 60000000);
            string accountNumber = number.ToString();
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
                AccountNumber = accountNumber,
                AccountName = "EnterBankX",
                Balance = 0,
                CreatedAt = DateTime.Now,
                Address = "Pursaklar / Ankara",
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
            //TestEmail(1026, DateTime.Now);
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                     new Claim("UserId", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var jwtToken = GetToken(authClaims);
                var newJwtToken = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                };
                return Ok(newJwtToken);
                //returning the token...

            }
            return Unauthorized();
        }
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
        [Route("~/ChangePassword")]
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


        [HttpGet("~/ChangePassword")]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            return RedirectToAction("Index", "ChangePassword", new { token, email });
        }


        [HttpPost]
        [Route("EmailSend")]
        public IActionResult EmailSend(SendMailDto sendMailDto)
        {
            var message = new Message(new string[] { sendMailDto.email }, sendMailDto.subject, sendMailDto.text);
            _emailService.SendEmail(message);
            return Ok("Mail gönderildi");
        }



        [HttpPost]
        [Route("ChangePasswordNew")]
        public async Task<IActionResult> ChangePasswordNew([FromBody] ChangePasswordNewDto changePasswordDto)
        {


            var user = await _userManager.FindByIdAsync(changePasswordDto.Id);

            if (user != null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    return Ok("Password changed successfully");
                }
                else
                {
                    return BadRequest("Password change failed");
                }
            }

            return NotFound("User not found");
        }

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




}

