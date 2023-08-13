using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Bill
{
    public class CreateBillDto
    {      
        public string BillName { get; set; }
        public string SubNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
