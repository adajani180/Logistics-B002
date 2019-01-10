using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Transaction
{
    [Table("TransactionTracking")]
    public class Transaction
    {
        public int Id { get; set; } // int - not null

        public int ItemNum { get; set; } // int - not null - FK

        public int EquipNum { get; set; } // int - FK
        public string TransactionType { get; set; } // string
        public int Qty { get; set; } // int
        public string EmpId { get; set; } // string

        public int NewEmpId { get; set; } // bigint - FK
        public string Vin { get; set; } // string
        public string UnitId { get; set; } // string
        public string ModifiedBy { get; set; } // string
        public DateTime ModifiedDate { get; set; } // datetime
    }
}