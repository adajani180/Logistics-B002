using Logistics.Entities.Error;
using System.Data.Entity;

namespace Logistics.Modal
{
    public class ErrorLogContext : DbContext
    {
        public ErrorLogContext() 
            : base("name=SatisDB") 
        {
        }

        public virtual DbSet<ErrorLog> Errors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }        
    }
}