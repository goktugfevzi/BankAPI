using DTOLayer.DTOs.TransactionType;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITransactionTypeService 
    {
        void TInsert(CreateTransactionTypeDto t);
        void TDelete(ResultTransactionTypeDto t);
        void TUpdate(UpdateTransactionTypeDto t);
        ResultTransactionTypeDto TGetById(int id);
        List<ResultTransactionTypeDto> TGetListAll();
    }
}
