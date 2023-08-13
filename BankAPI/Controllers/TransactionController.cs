using BusinessLayer.Abstract;
using DTOLayer.DTOs.Card;
using DTOLayer.DTOs.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult TransactionListByAccount(string accountNumber)
        {
            var values = _transactionService.TGetTransactionByAccountID(accountNumber);
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddTransaction(CreateTransactionDto createTransactionDto)
        {
            _transactionService.TInsert(createTransactionDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var value = _transactionService.TGetById(id);
            _transactionService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetTransaction(int id)
        {
            var value = _transactionService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateTransaction(UpdateTransactionDto updateTransactionDto)
        {
            _transactionService.TUpdate(updateTransactionDto);
            return Ok();
        }
    }
}
