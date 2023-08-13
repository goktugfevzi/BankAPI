using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DTOLayer.DTOs.Account;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFAccountDal : IAccountDal
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public EFAccountDal(IMapper mapper, BankContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Delete(ResultAccountDto t)
        {
            var value=_context.Accounts.Find(t.AccountID);
            _context.Accounts.Remove(value);
            _context.SaveChanges();
        }

        public ResultAccountDto GetById(int id)
        {
            var value= _context.Accounts.Find(id);
            return _mapper.Map<ResultAccountDto>(value);
        }

        public List<ResultAccountDto> GetListAll()
        {
            var value = _context.Accounts.ToList();
            return _mapper.Map<List<ResultAccountDto>>(value);
        }

        public void Insert(CreateAccountDto t)
        {
            var value=_mapper.Map<Account>(t);
            value.CreatedAt= DateTime.Now;
            _context.Accounts.Add(value);
            _context.SaveChanges();
        }

        public void Update(UpdateAccountDto t)
        {
            var acc = _context.Accounts.Find(t.AccountID);
            acc.Balance = t.Balance;
            _context.Accounts.Update(acc);
            _context.SaveChanges();
        }
    }
}
