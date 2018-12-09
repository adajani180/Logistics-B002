using Logistics.Areas.Config.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Logistics.Areas.Inventory.Entities
{
    [Table("Asset")]
    public class Asset
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string SerialNumber { get; set; }
        public int? Quantity { get; set; }
        public int? Issued { get; set; }
        [Column(TypeName = "Money")]
        public decimal? Price { get; set; }

        public long? StatusLookupId { get; set; }
        [ForeignKey("StatusLookupId")]
        public virtual ConfigLookup StatusLookup { get; set; }

        public long? TypeLookupId { get; set; }
        [ForeignKey("TypeLookupId")]
        public virtual ConfigLookup TypeLookup { get; set; }

        public long? BinId { get; set; }
        [ForeignKey("BinId")]
        public virtual Bin Bin { get; set; }

        public long? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        private DateTime? _DateAcquired;
        public DateTime? DateAcquired
        {
            get { return _DateAcquired == default(DateTime) ? null : _DateAcquired; }
            set => _DateAcquired = value;
        }
        public int? Threshold { get; set; }
        public string Notes { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}