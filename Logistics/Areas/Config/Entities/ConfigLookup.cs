using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Areas.Config.Entities
{
    [Table("ConfigLookup")]
    public class ConfigLookup
    {
        public long Id { get; set; } = 0;
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [StringLength(4)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Type { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        //[ForeignKey("CreatedBy")]
        //public virtual SystemUser CreatedByUser { get; set; }
        //[ForeignKey("ModifiedBy")]
        //public virtual SystemUser ModifiedByUser { get; set; }
    }
}