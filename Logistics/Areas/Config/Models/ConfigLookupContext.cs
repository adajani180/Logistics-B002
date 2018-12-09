using Logistics.Areas.Config.Entities;
using System.Data.Entity;

namespace Logistics.Areas.Config.Models
{
    public class ConfigLookupContext : DbContext
    {
        public ConfigLookupContext() 
            : base("name=SatisDB") 
        {
        }

        public virtual DbSet<ConfigLookup> ConfigLookups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}