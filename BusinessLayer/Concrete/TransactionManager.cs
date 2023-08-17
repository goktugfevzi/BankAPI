using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer.DTOs.Operations;
using DTOLayer.DTOs.TransactionDto;
using DTOLayer.DTOs.TransactionDto;
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
        private readonly ITransactionDal _transactionDal;

        public TransactionManager(ITransactionDal transactionDal)
        {
            _transactionDal = transactionDal;
        }

        public void TDelete(ResultTransactionDto t)
        {
            _transactionDal.Delete(t);
        }

        public void TDeposit(DepositDto depositDto)
        {
            _transactionDal.Deposit(depositDto);
        }

        public ResultTransactionDto TGetById(int id)
        {
            return _transactionDal.GetById(id);
        }

        public List<ResultTransactionDto> TGetListAll()
        {
            return _transactionDal.GetListAll();
        }

        public List<Transaction> TGetTransactionByAccountNumber(string accountNumber)
        {
            return _transactionDal.GetTransactionByAccountNumber(accountNumber);
        }

        public void TInsert(CreateTransactionDto t)
        {
            _transactionDal.Insert(t);
        }

        public void TSendMoney(SendMoneyDto sendMoneyDto)
        {
            _transactionDal.SendMoney(sendMoneyDto);
        }

        public void TUpdate(UpdateTransactionDto t)
        {
            _transactionDal.Update(t);
        }
    }
}
