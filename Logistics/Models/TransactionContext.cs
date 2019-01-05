using System.Data.Entity;
using Logistics.Entities.Transaction;

namespace Logistics.Models
{
    public class TransactionContext : DbContext
    {
        public TransactionContext()
            : base("name=SatisDB")
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }
}