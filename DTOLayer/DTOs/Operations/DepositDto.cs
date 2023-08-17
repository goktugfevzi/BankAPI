using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Operations
{
    public class DepositDto
    {       
        public string SenderAccountNumber { get; set; }       
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeID { get; set; }
    }
}
