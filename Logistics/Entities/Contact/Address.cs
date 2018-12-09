using Logistics.Areas.Config.Entities;
using Logistics.Areas.Inventory.Entities;
using Logistics.Entities.Personnel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Contact
{
    [Table("Address")]
    public class Address
    {
        public long Id { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public long StateId { get; set; }
        public string ZipCode { get; set; }

        public long TypeLookupId { get; set; }
        [ForeignKey("TypeLookupId")]
        public virtual ConfigLookup TypeLookup { get; set; }

        public long? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        public long? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}