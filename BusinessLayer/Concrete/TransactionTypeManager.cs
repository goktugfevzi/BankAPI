using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer.DTOs.TransactionType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TransactionTypeManager : ITransactionTypeService
    {
        private readonly ITransactionTypeDal _transactionTypeDal;

        public TransactionTypeManager(ITransactionTypeDal transactionTypeDal)
        {
            _transactionTypeDal = transactionTypeDal;
        }

        public void TDelete(ResultTransactionTypeDto t)
        {
           _transactionTypeDal.Delete(t);
        }

        public ResultTransactionTypeDto TGetById(int id)
        {
            return _transactionTypeDal.GetById(id);
        }

        public List<ResultTransactionTypeDto> TGetListAll()
        {
            return _transactionTypeDal.GetListAll();
        }

        public void TInsert(CreateTransactionTypeDto t)
        {
            _transactionTypeDal.Insert(t);
        }

        public void TUpdate(UpdateTransactionTypeDto t)
        {
            _transactionTypeDal.Update(t);
        }
    }
}
