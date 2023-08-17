using DTOLayer.DTOs.Card;
using DTOLayer.DTOs.Operations;
using DTOLayer.DTOs.TransactionDto;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITransactionDal
    {
        void Insert(CreateTransactionDto t);
        void Delete(ResultTransactionDto t);
        void Update(UpdateTransactionDto t);
        ResultTransactionDto GetById(int id);
        List<ResultTransactionDto> GetListAll();
        List<Transaction> GetTransactionByAccountNumber(string AccountNumber);
        void Deposit(DepositDto depositDto);
        void SendMoney(SendMoneyDto sendMoneyDto);


    }
}
