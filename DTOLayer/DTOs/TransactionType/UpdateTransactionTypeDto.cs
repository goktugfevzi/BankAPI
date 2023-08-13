using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.TransactionType
{
    public class UpdateTransactionTypeDto
    {
        public int TransactionTypeID { get; set; }
        public string TransactionTypeName { get; set; }
    }
}
