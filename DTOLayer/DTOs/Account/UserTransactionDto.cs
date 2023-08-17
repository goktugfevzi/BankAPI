using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Account
{
    public class UserTransactionDto
    {
        public User user { get; set; }
        public List<Transaction> transactions { get; set; }
    }
}
