using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DTOLayer.DTOs.Bill;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFBillDal : IBillDal
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public EFBillDal(BankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Delete(ResultBillDto t)
        {
            var value = _context.Bills.Find(t.BillID);
            _context.Bills.Remove(value);
            _context.SaveChanges();
        }

        public ResultBillDto GetById(int id)
        {
            var value = _context.Bills.Find(id);
            return _mapper.Map<ResultBillDto>(value);
        }

        public List<ResultBillDto> GetListAll()
        {
            var value = _context.Bills.ToList();            
            return _mapper.Map<List<ResultBillDto>>(value);
        }

        public void Insert(CreateBillDto t)
        {
            var bill = _mapper.Map<Bill>(t);          
            _context.Bills.Add(bill);
            _context.SaveChanges();
        }

        public void Update(UpdateBillDto t)
        {
            var value = _mapper.Map<Bill>(t);
            _context.Bills.Update(value);
            _context.SaveChanges();
        }
    }
}
