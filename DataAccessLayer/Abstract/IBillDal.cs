using DTOLayer.DTOs.Bill;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IBillDal 
    {
        void Insert(CreateBillDto t);
        void Delete(ResultBillDto t);
        void Update(UpdateBillDto t);
        ResultBillDto GetById(int id);
        List<ResultBillDto> GetListAll();
    }
}
