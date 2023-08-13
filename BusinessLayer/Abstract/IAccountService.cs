using DTOLayer.DTOs.Account;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAccountService 
    {
        void TInsert(CreateAccountDto t);
        void TDelete(ResultAccountDto t);
        void TUpdate(UpdateAccountDto t);
        ResultAccountDto TGetById(int id);
        List<ResultAccountDto> TGetListAll();
    }
}
