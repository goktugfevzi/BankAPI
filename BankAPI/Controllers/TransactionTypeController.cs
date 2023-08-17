using BusinessLayer.Abstract;
using DTOLayer.DTOs.TransactionDto;
using DTOLayer.DTOs.TransactionType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeService)
        {
            _transactionTypeService = transactionTypeService;
        }

        [HttpGet]
        public IActionResult TransactionTypeList()
        {
            var values = _transactionTypeService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddTransactionType(CreateTransactionTypeDto createTransactionTypeDto)
        {
            _transactionTypeService.TInsert(createTransactionTypeDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransactionType(int id)
        {
            var value = _transactionTypeService.TGetById(id);
            _transactionTypeService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetTransactionType(int id)
        {
            var value = _transactionTypeService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateTransactionType(UpdateTransactionTypeDto updateTransactionTypeDto)
        {
            _transactionTypeService.TUpdate(updateTransactionTypeDto);
            return Ok();
        }
    }
}
