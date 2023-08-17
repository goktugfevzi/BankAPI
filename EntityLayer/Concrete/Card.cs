using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public string CardType { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardCvc { get; set; }



        [ForeignKey(nameof(User))]
        public int Id { get; set; }
        public User User { get; set; }
    }
}
