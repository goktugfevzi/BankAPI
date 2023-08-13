using DTOLayer.DTOs.Transaction;
using DTOLayer.DTOs.TransactionType;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITransactionTypeDal 
    {
        void Insert(CreateTransactionTypeDto t);
        void Delete(ResultTransactionTypeDto t);
        void Update(UpdateTransactionTypeDto t);
        ResultTransactionTypeDto GetById(int id);
        List<ResultTransactionTypeDto> GetListAll();
    }
}
