using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TransactionManager : ITransactionService
    {
        public void TDelete(Transaction t)
        {
            throw new NotImplementedException();
        }

        public Transaction TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> TGetListAll()
        {
            throw new NotImplementedException();
        }

        public void TInsert(Transaction t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Transaction t)
        {
            throw new NotImplementedException();
        }
    }
}
