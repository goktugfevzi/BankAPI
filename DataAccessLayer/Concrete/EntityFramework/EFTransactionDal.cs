using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DTOLayer.DTOs.Card;
using DTOLayer.DTOs.Transaction;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFTransactionDal : ITransactionDal
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public EFTransactionDal(BankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Delete(ResultTransactionDto t)
        {
            var value = _context.Transactions.Find(t.TransactionID);
            _context.Transactions.Remove(value);
            _context.SaveChanges();
        }

        public ResultTransactionDto GetById(int id)
        {
            var value = _context.Transactions.Find(id);
            return _mapper.Map<ResultTransactionDto>(value);
        }

        public List<ResultTransactionDto> GetListAll()
        {
            var value = _context.Transactions.ToList();
            return _mapper.Map<List<ResultTransactionDto>>(value);
        }

        public List<ResultTransactionDto> GetTransactionByAccountID(string accountnumber)
        {
            var value = _context.Transactions.Where(x => x.SenderAccountNumber == accountnumber).ToList();
            return _mapper.Map<List<ResultTransactionDto>>(value);
        }

        public void Insert(CreateTransactionDto t)
        {
            var value = _mapper.Map<Transaction>(t);
            _context.Transactions.Add(value);
            _context.SaveChanges();
        }

        public void Update(UpdateTransactionDto t)
        {
            var value = _mapper.Map<Transaction>(t);
            _context.Transactions.Update(value);
            _context.SaveChanges();
        }
    }
}
