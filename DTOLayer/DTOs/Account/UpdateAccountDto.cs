using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Account
{
    public class UpdateAccountDto
    {
        public int AccountID { get; set; }
        public int UserId { get; set; }        
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
       
    }
}
