using DTOLayer.DTOs.Transaction;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITransactionService
    {
        void TInsert(CreateTransactionDto t);
        void TDelete(ResultTransactionDto t);
        void TUpdate(UpdateTransactionDto t);
        ResultTransactionDto TGetById(int id);
        List<ResultTransactionDto> TGetListAll();
        List<ResultTransactionDto> TGetTransactionByAccountID(string accountNumber);
    }
}
