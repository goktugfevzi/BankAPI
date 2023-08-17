using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Auth
{
    public class ChangePasswordNewDto
    {
        public string OldPassword { get; set; }
        public string Id { get; set; }
        public string NewPassword { get; set; }
    }
}
