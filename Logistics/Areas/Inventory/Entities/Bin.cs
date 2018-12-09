using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Areas.Inventory.Entities
{
    [Table("Bin")]
    public class Bin
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public long? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}