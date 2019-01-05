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
        public void Delete(Transaction entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Transaction Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Transaction entity)
        {
            throw new NotImplementedException();
        }
    }
}