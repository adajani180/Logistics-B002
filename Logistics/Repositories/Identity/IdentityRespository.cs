using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logistics.Repositories.Interface;
using Logistics.Models;

namespace Logistics.Repositories.Identity
{
    public class IdentityRespository : IRepository<Identity>       
    {

        ApplicationDbContext context = new ApplicationDbContext();



        public void Delete(Identity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Identity> Find(System.Linq.Expressions.Expression<Func<Identity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Identity Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Identity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Identity entity)
        {
            throw new NotImplementedException();
        }
    }
}