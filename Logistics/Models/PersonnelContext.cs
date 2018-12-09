using Logistics.Entities.Contact;
using Logistics.Entities.Personnel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Logistics.Models
{
    public class PersonnelContext : DbContext
    {
        public PersonnelContext() 
            : base("name=SatisDB") 
        {
        }

        public virtual DbSet<Person> Personnel { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<EmailAddress> Emails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Person>()
            //    .HasMany(person => person.Addresses)
            //    .WithOptional()
            //    .HasForeignKey(address => address.PersonId);
            //modelBuilder.Entity<Address>()
            //    .HasKey(address => new { address.Id, address.PersonId })
            //    .Property(address => address.Id)
            //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}