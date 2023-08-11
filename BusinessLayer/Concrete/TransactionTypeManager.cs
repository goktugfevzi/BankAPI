using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TransactionTypeManager : ITransactionTypeService
    {
        public void TDelete(EntityLayer.Concrete.TransactionType t)
        {
            throw new NotImplementedException();
        }

        public EntityLayer.Concrete.TransactionType TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<EntityLayer.Concrete.TransactionType> TGetListAll()
        {
            throw new NotImplementedException();
        }

        public void TInsert(EntityLayer.Concrete.TransactionType t)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(EntityLayer.Concrete.TransactionType t)
        {
            throw new NotImplementedException();
        }
    }
}
