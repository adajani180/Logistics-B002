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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}