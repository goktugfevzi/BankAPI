using DTOLayer.DTOs.Card;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICardService 
    {
        void TInsert(CreateCardDto t);
        void TDelete(ResultCardDto t);
        void TUpdate(UpdateCardDto t);
        ResultCardDto TGetById(int id);
        List<ResultCardDto> TGetListAll();
    }
}
