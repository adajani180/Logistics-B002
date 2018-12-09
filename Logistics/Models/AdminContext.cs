using Logistics.Areas.Config.Entities;
using Logistics.Entities.Access;
using System.Data.Entity;

namespace Logistics.Modal
{
    public class AdminContext : DbContext
    {
        public AdminContext() 
            : base("name=SatisDB") 
        {
            //Database.SetInitializer<AccessContext>(new CreateDatabaseIfNotExists<AccessContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new SchoolDBInitializer());
        }

        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<ConfigLookup> ConfigLookups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SystemUser>()
            //    .Property(e => e.UserName);
        }        
    }
}