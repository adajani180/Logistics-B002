using Logistics.Areas.Config.Entities;
using Logistics.Areas.Inventory.Entities;
using System.Data.Entity;

namespace Logistics.Areas.Config.Models
{
    public class InventoryContext : DbContext
    {
        public InventoryContext() 
            : base("name=SatisDB") 
        {
        }

        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Bin> Bins { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<ConfigLookup> ConfigLookups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Warehouse>()
            //    .HasMany<Bin>(wh => wh.Bins);

            //modelBuilder.Entity<Warehouse>()
            //    .HasMany<Bin>(warehouse => warehouse.Bins);

            //modelBuilder.Entity<Bin>()
            //    .HasOptional<Warehouse>(bin => bin.Warehouse);
        }
    }
}