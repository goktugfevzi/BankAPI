using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Bill
    {
        public int BillID { get; set; }
        public string BillName { get; set; }
        public string SubNumber { get; set; }
        public decimal Amount { get; set; }
    }    
}
