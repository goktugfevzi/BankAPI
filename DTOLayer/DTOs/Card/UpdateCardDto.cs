using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Card
{
    public class UpdateCardDto
    {
        public int CardId { get; set; }
        public int Id { get; set; }

        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public string CardType { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardCvc { get; set; }
    }
}
