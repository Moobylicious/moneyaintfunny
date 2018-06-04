using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneyAintFunny.Core.Base.Interfaces;
using MoneyAintFunny.Data.Models;

namespace MoneyAintFunny.Core.Data.Context
{

    public class MoneyAintFunnyContext : IdentityDbContext<IdentityUser>, IMoneyAintFunnyContext
    {
        public MoneyAintFunnyContext(DbContextOptions<MoneyAintFunnyContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TransactionRecord> TransactionRecords { get; set; }
    }
}
