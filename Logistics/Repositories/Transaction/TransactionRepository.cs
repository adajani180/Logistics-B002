using Logistics.Entities;
using Logistics.Entities.Transaction;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        TransactionContext context = new TransactionContext();

        public void Delete(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }


        // Find a Transaction
        public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate) => this.context.Transactions.AsNoTracking().Where(predicate);


        // Get a Single Transaction
        public Transaction Get(object id)
        {
            int tranId = int.Parse(id.ToString());

            var tran = this.Find(t => t.Id == tranId).SingleOrDefault();

            return tran;
        }

        // Get All Transactions
        public IEnumerable<Transaction> GetAll() => this.context.Transactions;


        // Create or Save a Transaction
        public void Save(Transaction entity)
        {
            Transaction tran = this.Get(entity.Id);
            if (tran == null)
            {
                entity.ItemNum = tran.ItemNum;
                entity.EmpId = tran.EmpId;
                entity.EquipNum = tran.EquipNum;
                entity.ModifiedBy = null;
                entity.ModifiedDate = DateTime.Now;
                entity.NewEmpId = tran.NewEmpId;
                entity.Qty = tran.Qty;
                entity.TransactionType = tran.TransactionType;
                entity.UnitId = tran.UnitId;
                entity.Vin = tran.Vin;

                this.context.Transactions.Add(entity);
            }
            else
            {
                entity.ItemNum = tran.ItemNum;
                entity.EmpId = tran.EmpId;
                entity.EquipNum = tran.EquipNum;
                entity.ModifiedBy = tran.ModifiedBy;
                entity.ModifiedDate = DateTime.Now;
                entity.NewEmpId = tran.NewEmpId;
                entity.Qty = tran.Qty;
                entity.TransactionType = tran.TransactionType;
                entity.UnitId = tran.UnitId;
                entity.Vin = tran.Vin;

                this.context.Entry(tran).State = EntityState.Modified;
            }
            
            this.context.SaveChanges();
        }
    }
}