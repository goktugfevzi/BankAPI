using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer.DTOs.Account;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDal _accountDal;

        public AccountManager(IAccountDal accountDal)
        {
            _accountDal = accountDal;
        }

        public void TDelete(ResultAccountDto t)
        {
            _accountDal.Delete(t);
        }

        public ResultAccountDto TGetById(int id)
        {
            return _accountDal.GetById(id);
        }

        public List<ResultAccountDto> TGetListAll()
        {
            return _accountDal.GetListAll();
        }

        public void TInsert(CreateAccountDto t)
        {
            _accountDal.Insert(t);
        }

        public void TUpdate(UpdateAccountDto t)
        {
            _accountDal.Update(t);
        }
    }
}
