using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities
{
    [Table("Exam")]
    public class Exam
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExpirationCycle { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //public virtual List<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
}