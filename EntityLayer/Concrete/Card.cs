using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Card
    {
        public int CardId { get; set; }
        public int AccountID { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardCvc { get; set; }

        public Account Account { get; set; }
    }
}
