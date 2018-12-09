using Logistics.Areas.Config.Entities;
using Logistics.Areas.Inventory.Entities;
using Logistics.Entities.Personnel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities
{
    [Table("ExamResult")]
    public class ExamResult
    {
        public long Id { get; set; } = 0;
        public DateTime ExamDate { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Notes { get; set; }

        public long ExamId { get; set; }
        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        public long? AssetId { get; set; }
        [ForeignKey("AssetId")]
        public virtual Asset Asset { get; set; }

        public long? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public long ResultLookupId { get; set; }
        [ForeignKey("ResultLookupId")]
        public virtual ConfigLookup ResultLookup { get; set; }
    }
}