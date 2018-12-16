using System;
using System.Collections.Generic;
using System.Linq;
using Logistics.Entities;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Logistics.Repositories.Identity
{
    public class IdentityRepository : IRepository<Entities.Identity>
    {
        public void Delete(Entities.Identity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.Identity> Find(Expression<Func<Entities.Identity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Entities.Identity Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entities.Identity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Entities.Identity entity)
        {
            throw new NotImplementedException();
        }
    }
}