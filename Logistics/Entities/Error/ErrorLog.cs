using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Error
{
    [Table("ErrorLog")]
    public class ErrorLog
    {
        public long Id { get; set; } = 0;
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}