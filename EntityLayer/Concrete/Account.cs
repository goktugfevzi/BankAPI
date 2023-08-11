using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Account
    {
        
        public int AccountID { get; set; }
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
