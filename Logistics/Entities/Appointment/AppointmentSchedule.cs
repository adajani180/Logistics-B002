using Logistics.Areas.Config.Entities;
using Logistics.Entities.Personnel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Appointment
{
    [Table("AppointmentSchedule")]
    public class AppointmentSchedule
    {
        public long Id { get; set; }
        public string Notes { get; set; }

        public long AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public virtual Appointment Appointment { get; set; }
        
        public long? StatusLookupId { get; set; }
        [ForeignKey("StatusLookupId")]
        public virtual ConfigLookup StatusLookup { get; set; }

        public long? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public long? ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}