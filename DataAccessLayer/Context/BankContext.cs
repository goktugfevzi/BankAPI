using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class BankContext : IdentityDbContext<User, Role, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-ITA1D3N\\SQLEXPRESS;database= BankApiDB;integrated security = true");

        }


        //    optionsBuilder.UseSqlServer("server=FRKN\\SQLEXPRESS;database= BankApiDB;integrated security = true");
        //    optionsBuilder.UseSqlServer("server=DESKTOP-ITA1D3N\\SQLEXPRESS;database= BankApiDB;integrated security = true");
        // optionsBuilder.UseSqlServer("server=bankappserver.database.windows.net;database=BankApiDB;user=BankAdmin;password=Admin123;Connection Timeout=30;TrustServerCertificate=False;");





        public DbSet<Bill> Bills { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

    }
}
