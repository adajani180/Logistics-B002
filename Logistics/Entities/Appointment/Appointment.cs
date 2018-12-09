using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Appointment
{
    [Table("Appointment")]
    public class Appointment
    {
        public long Id { get; set; } = 0;
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //[ScriptIgnore]
        //public virtual List<AppointmentSchedule> AppointmentSchedules { get; set; } = new List<AppointmentSchedule>();
    }
}