using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Auth
{
    public class SendMailDto
    {
        public string text { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
    }
}
