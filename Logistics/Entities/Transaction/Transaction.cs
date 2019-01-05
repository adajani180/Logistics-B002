using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Logistics.Entities.Transaction
{
    [Table("TransactionTracking")]
    public class Transaction
    {
        public int Id { get; set; } // int - not null

        [ForeignKey("")]
        public int ItemNum { get; set; } // int - not null

        [ForeignKey("")]
        public int? EquipNum { get; set; } // int
        public string? TransactionType { get; set; } // string
        public int? Qty { get; set; } // int
        public string? EmpId { get; set; } // string

        [ForeignKey("")]
        public int? NewEmpId { get; set; } // bigint
        public string? Vin { get; set; } // string
        public string? UnitId { get; set; } // string
        public string? ModifiedBy { get; set; } // string
        public DateTime? ModifiedDate { get; set; } // datetime


    }
}