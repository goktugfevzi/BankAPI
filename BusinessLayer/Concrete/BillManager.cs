using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DTOLayer.DTOs.Bill;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BillManager : IBillService
    {
        private readonly IBillDal _billDal;

        public BillManager(IBillDal billDal)
        {
            _billDal = billDal;
        }

        public void TDelete(ResultBillDto t)
        {
            _billDal.Delete(t);
        }

        public ResultBillDto TGetById(int id)
        {
            return _billDal.GetById(id);
        }

        public List<ResultBillDto> TGetListAll()
        {
            return _billDal.GetListAll();
        }

        public void TInsert(CreateBillDto t)
        {
            _billDal.Insert(t);
        }

        public void TUpdate(UpdateBillDto t)
        {
            _billDal.Update(t);
        }
    }
}
