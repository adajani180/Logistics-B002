using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logistics.Repositories.Interface;
using Logistics.Models;
using System.Linq.Expressions;

namespace Logistics.Repositories.Identity
{
    public class IdentityRepository : IRepository<ApplicationUser>
    {
        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> Find(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}