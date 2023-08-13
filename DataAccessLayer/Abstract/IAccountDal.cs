using DTOLayer.DTOs.Account;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IAccountDal 
    {
        void Insert(CreateAccountDto t);
        void Delete(ResultAccountDto t);
        void Update(UpdateAccountDto t);
        ResultAccountDto GetById(int id);
        List<ResultAccountDto> GetListAll();
    }
}
