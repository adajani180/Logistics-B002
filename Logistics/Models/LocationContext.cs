using Logistics.Entities;
using System.Data.Entity;

namespace Logistics.Models
{
    public class LocationContext : DbContext
    {
        public LocationContext()
            : base("name=SatisDB")
        {
        }

        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}