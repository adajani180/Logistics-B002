using System;
using System.Collections.Generic;
using System.Linq;
using Logistics.Entities;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Logistics.Repositories.Identity
{
    public class IdentityRepository : IRepository<IdentityUser>
    {
        // DB Context
        ApplicationDbContext context = new ApplicationDbContext();

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdentityUser> Find(Expression<Func<IdentityUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            UserManager _userManager = new UserManager();
            return _userManager.Users.ToList();
        }

        public void Save(IdentityUser entity)
        {
            throw new NotImplementedException();
        }

        IdentityUser IRepository<IdentityUser>.Get(object id)
        {
            throw new NotImplementedException();
        }
    }

    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        { }
    }

    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager()
            : base(new RoleStore<IdentityRole>(new ApplicationDbContext()))
        { }
    }
}