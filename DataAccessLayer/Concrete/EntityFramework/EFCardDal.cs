using AutoMapper;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DTOLayer.DTOs.Bill;
using DTOLayer.DTOs.Card;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class EFCardDal : ICardDal
    {
        private readonly BankContext _context;
        private readonly IMapper _mapper;

        public EFCardDal(BankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Delete(ResultCardDto t)
        {
            var value = _context.Cards.Find(t.CardId);
            _context.Cards.Remove(value);
            _context.SaveChanges();
        }

        public ResultCardDto GetById(int id)
        {
            var value = _context.Cards.Find(id);
            return _mapper.Map<ResultCardDto>(value);
        }

        public List<ResultCardDto> GetListAll()
        {
            var value = _context.Cards.ToList();
            return _mapper.Map<List<ResultCardDto>>(value);
        }

        public List<ResultCardDto> GetListByAccount(int id)
        {
           var value=_context.Cards.Where(x=>x.Id== id).ToList();
            return _mapper.Map<List<ResultCardDto>>(value);
        }

        public void Insert(CreateCardDto t)
        {
            var value = _mapper.Map<Card>(t);
            _context.Cards.Add(value);
            _context.SaveChanges();
        }

        public void Update(UpdateCardDto t)
        {
            var value = _mapper.Map<Card>(t);
            _context.Cards.Update(value);
            _context.SaveChanges();
        }
    }
}
