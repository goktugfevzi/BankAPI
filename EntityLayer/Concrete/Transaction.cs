using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string SenderAccountNumber { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public decimal Amount { get; set; }       
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeID { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
