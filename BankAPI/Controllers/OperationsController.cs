using AutoMapper;
using BusinessLayer.Abstract;
using DTOLayer.DTOs.Account;
using DTOLayer.DTOs.Operations;

//using DTOLayer.DTOs.Operations;
using DTOLayer.DTOs.TransactionDto;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly UserManager<User> _userManager;
        private readonly IBillService _billService;
        private readonly IMapper _mapper;

        public OperationsController(ITransactionService transactionService, IMapper mapper, IBillService billService, UserManager<User> userManager)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _billService = billService;
            _userManager = userManager;
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



        [HttpPost("PayBill")]
        public async Task<IActionResult> PayBill(PayBillDto payBillDto)
        {

            var user = await _userManager.FindByIdAsync(payBillDto.UserID.ToString());

            var value = _billService.TGetById(payBillDto.BillId);
            if (user.Balance > value.Amount)
            {
                user.Balance = user.Balance - value.Amount;
                await _userManager.UpdateAsync(user);

                var newTransaction = new CreateTransactionDto
                {
                    TransactionTypeID = 3,
                    SenderAccountNumber = user.AccountNumber,
                    Amount = value.Amount,
                    TransactionDate = DateTime.Now
                };
                _transactionService.TInsert(newTransaction);
                return Ok();
            }
            return BadRequest("Faturayı Ödeyecek Miktarda Bakiye Yoktur.");
        }
    }
}