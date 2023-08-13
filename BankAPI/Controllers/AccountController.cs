using BusinessLayer.Abstract;
using DTOLayer.DTOs.Account;
using DTOLayer.DTOs.Bill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult AccountList()
        {
            var values = _accountService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddAccount(CreateAccountDto createAccountDto)
        {
            _accountService.TInsert(createAccountDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var value = _accountService.TGetById(id);
            _accountService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var value = _accountService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            _accountService.TUpdate(updateAccountDto);
            return Ok();
        }
    }
}
