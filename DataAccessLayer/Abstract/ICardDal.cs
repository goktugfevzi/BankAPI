using DTOLayer.DTOs.Card;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICardDal 
    {
        void Insert(CreateCardDto t);
        void Delete(ResultCardDto t);
        void Update(UpdateCardDto t);
        ResultCardDto GetById(int id);
        List<ResultCardDto> GetListAll();
    }
}
