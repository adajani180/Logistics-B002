using Logistics.Entities;
using System.Data.Entity;

namespace Logistics.Models
{
    public class ExamContext : DbContext
    {
        public ExamContext() 
            : base("name=SatisDB") 
        {
        }

        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<ExamResult> ExamResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Appointment>()
            //    .HasMany<AppointmentSchedule>(app => app.AppointmentSchedules)
            //    .WithRequired(appDetail => appDetail.Appointment)
            //    .HasForeignKey<long>(appDetail => appDetail.AppointmentId);
        }
    }
}