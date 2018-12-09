using System;

namespace Logistics.Entities.Appointment
{
    public class CalendarEvent
    {
        public long AppointmentId { get; set; }
        public long AppointmentScheduleId { get; set; } = 0;

        public string Location { get; set; }
        public string Notes { get; set; }

        public long StatusId { get; set; } = 0;
        public string StatusDescription { get; set; }

        public long PersonId { get; set; } = 0;
        public string PersonFullName { get; set; }

        public long ExamId { get; set; } = 0;

        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string AppointmentDateTime => $"{AppointmentDate.ToShortDateString()} {AppointmentTime.ToString(@"hh\:mm")}";
    }
}