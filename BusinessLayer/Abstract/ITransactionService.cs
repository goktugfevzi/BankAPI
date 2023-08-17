using DTOLayer.DTOs.Operations;
using DTOLayer.DTOs.TransactionDto;
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
        List<Transaction> TGetTransactionByAccountNumber(string accountNumber);
        void TDeposit(DepositDto depositDto); 
        void TSendMoney(SendMoneyDto sendMoneyDto);
    }
}
