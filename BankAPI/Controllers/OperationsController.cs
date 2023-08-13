using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer.DTOs.Account;
//using DTOLayer.DTOs.Operations;
using DTOLayer.DTOs.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public OperationsController(IAccountService accountService, ITransactionService transactionService, IMapper mapper)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        //[HttpPost("Deposit")]
        //public IActionResult Deposit(DepositDto depositDto)
        //{
        //    var value = _accountService.TGetById(depositDto.AccountID);
        //    value.Balance += depositDto.Balance;
        //    var result=_mapper.Map<UpdateAccountDto>(value);
        //    _accountService.TUpdate(result);
        //    return Ok("Hesabınıza Para Yatırıldı");
        //}

        [HttpPost("SendMoney")]
        public IActionResult SendMoney(CreateTransactionDto createTransactionDto)
        {
            createTransactionDto.TransactionDate = DateTime.Now;
            createTransactionDto.TransactionTypeID = 1;
            return Ok();
        }
    }
}
