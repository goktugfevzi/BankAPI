using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer.DTOs.Card;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CardManager : ICardService
    {
        private readonly ICardDal _carddal;

        public CardManager(ICardDal carddal)
        {
            _carddal = carddal;
        }

        public void TDelete(ResultCardDto t)
        {
            _carddal.Delete(t);
        }

        public ResultCardDto TGetById(int id)
        {
            return _carddal.GetById(id);
        }

        public List<ResultCardDto> TGetListAll()
        {
            return _carddal.GetListAll();
        }

        public void TInsert(CreateCardDto t)
        {
            _carddal.Insert(t);
        }

        public void TUpdate(UpdateCardDto t)
        {
            _carddal.Update(t);
        }
    }
}
