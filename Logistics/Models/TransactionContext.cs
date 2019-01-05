using System.Data.Entity;
using Logistics.Entities;

namespace Logistics.Models
{
    public class TransactionContext : DbContext
    {
        public TransactionContext()
            : base("name=SatisDB")
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }

        //public virtual DbSet<ID> Id{ get; set; } // int - not null
        //public virtual DbSet<ItemNum> ItemNum { get; set; } // int - not null
        //public virtual DbSet<EquipNum> EquipNum { get; set; } // int
        //public virtual DbSet<TransactionType> TransactionType { get; set; } // string
        //public virtual DbSet<Qty> Quantity { get; set; } // int
        //public virtual DbSet<EmpId> EmployeeId { get; set; } // string
        //public virtual DbSet<NewEmpId> NewEmployeeId { get; set; } // bigint
        //public virtual DbSet<Vin> Vin { get; set; } // string
        //public virtual DbSet<UnitId> UnitId { get; set; } // string
        //public virtual DbSet<ModifiedBy> ModifiedBy { get; set; } // string
        //public virtual DbSet<ModifiedDate> ModifiedDate { get; set; } // datetime

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }
}