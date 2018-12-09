using Logistics.Entities.Appointment;
using System.Data.Entity;

namespace Logistics.Models
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext() 
            : base("name=SatisDB") 
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentSchedule> AppointmentSchedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Appointment>()
            //    .HasMany<AppointmentSchedule>(app => app.AppointmentSchedules)
            //    .WithRequired(appDetail => appDetail.Appointment)
            //    .HasForeignKey<long>(appDetail => appDetail.AppointmentId);
        }
    }
}