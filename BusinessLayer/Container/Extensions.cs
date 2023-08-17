using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {

            services.AddScoped<IBillService, BillManager>();
            services.AddScoped<IBillDal, EFBillDal>();

            services.AddScoped<ICardService, CardManager>();
            services.AddScoped<ICardDal, EFCardDal>();

            services.AddScoped<ITransactionService, TransactionManager>();
            services.AddScoped<ITransactionDal, EFTransactionDal>();

            services.AddScoped<ITransactionTypeService, TransactionTypeManager>();
            services.AddScoped<ITransactionTypeDal, EFTransactionTypeDal>();

        }

    }
}
