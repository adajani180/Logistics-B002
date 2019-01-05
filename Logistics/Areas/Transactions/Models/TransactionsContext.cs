using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Logistics.Areas.Transactions.Entities;

namespace Logistics.Areas.Transactions.Models
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext() : base("name=SatisDB")
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        
    }
}