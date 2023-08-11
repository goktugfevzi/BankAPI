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
    public class BankContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=bankappserver.database.windows.net;initial catalog=BankApiDB; User ID=BankAdmin;Password=Admin123;");
        }
        //;TrustServerCertificate=False;Connection Timeout=30;

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

    }
}
