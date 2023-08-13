using BusinessLayer.Abstract;
using DTOLayer.DTOs.Bill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public IActionResult BillList()
        {
            var values = _billService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddBill(CreateBillDto createBillDto)
        {
            _billService.TInsert(createBillDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            var value = _billService.TGetById(id);
            _billService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetBill(int id)
        {
            var value = _billService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateBill(UpdateBillDto updateBillDto)
        {
            _billService.TUpdate(updateBillDto);
            return Ok();
        }
    }
}
