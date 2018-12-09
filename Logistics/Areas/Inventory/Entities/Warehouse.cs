using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Areas.Inventory.Entities
{
    [Table("Warehouse")]
    public class Warehouse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<Bin> Bins { get; set; }
    }
}