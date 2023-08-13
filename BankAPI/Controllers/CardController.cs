using BusinessLayer.Abstract;
using DTOLayer.DTOs.Bill;
using DTOLayer.DTOs.Card;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public IActionResult CardList()
        {
            var values = _cardService.TGetListAll();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddCard(CreateCardDto createCardDto)
        {
            _cardService.TInsert(createCardDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCard(int id)
        {
            var value = _cardService.TGetById(id);
            _cardService.TDelete(value);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetCard (int id)
        {
            var value = _cardService.TGetById(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateCard(UpdateCardDto updateCardDto)
        {
            _cardService.TUpdate(updateCardDto);
            return Ok();
        }
    }
}
