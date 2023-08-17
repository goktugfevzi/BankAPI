using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DTOLayer.DTOs.TransactionDto;
using DTOLayer.DTOs.TransactionType;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFTransactionTypeDal : ITransactionTypeDal
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public EFTransactionTypeDal(BankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Delete(ResultTransactionTypeDto t)
        {
            var value = _context.TransactionTypes.Find(t.TransactionTypeID);
            _context.TransactionTypes.Remove(value);
            _context.SaveChanges();
        }

        public ResultTransactionTypeDto GetById(int id)
        {
            var value = _context.TransactionTypes.Find(id);
            return _mapper.Map<ResultTransactionTypeDto>(value);
        }

        public List<ResultTransactionTypeDto> GetListAll()
        {
            var value = _context.TransactionTypes.ToList();
            return _mapper.Map<List<ResultTransactionTypeDto>>(value);
        }

        public void Insert(CreateTransactionTypeDto t)
        {
            var value = _mapper.Map<TransactionType>(t);
            _context.TransactionTypes.Add(value);
            _context.SaveChanges();
        }

        public void Update(UpdateTransactionTypeDto t)
        {
            var value = _mapper.Map<TransactionType>(t);
            _context.TransactionTypes.Update(value);
            _context.SaveChanges(); ;
        }
    }
}
