using DTOLayer.DTOs.Bill;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBillService 
    {
        void TInsert(CreateBillDto t);
        void TDelete(ResultBillDto t);
        void TUpdate(UpdateBillDto t);
        ResultBillDto TGetById(int id);
        List<ResultBillDto> TGetListAll();
    }
}
