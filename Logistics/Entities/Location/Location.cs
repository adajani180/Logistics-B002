using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities
{
    [Table("Location")]
    public class Location
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}