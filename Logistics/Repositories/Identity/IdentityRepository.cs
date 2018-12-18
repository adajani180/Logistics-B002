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
using System.Collections;

namespace Logistics.Repositories.Identity
{
    public class IdentityRepository : IRepository<IdentityUser>
    {
        // DB Context
        ApplicationDbContext context = new ApplicationDbContext();

        public void Delete(IdentityUser entity)
        {
            UserManager _userManager = new UserManager();

            string userId = this.Get(entity.Id).ToString();        
            ApplicationUser user = _userManager.FindById(userId);

            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<IdentityUser>().Attach(entity);

            this.context.Users.Remove(user);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            UserManager _userManager = new UserManager();

            string userId = id.ToString();
            ApplicationUser user = _userManager.FindById(userId);

            this.Delete(user);
        }

        public IEnumerable<IdentityUser> Find(Expression<Func<IdentityUser, bool>> predicate) => this.context.Users.AsNoTracking().Where(predicate);


        public IdentityUser Get(object id)
        {
            UserManager _userManager = new UserManager();

            string userId = id.ToString();
            ApplicationUser user = _userManager.FindById(userId);

            return user;
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            UserManager _userManager = new UserManager();
            return _userManager.Users.ToList();
        }

        public void Save(IdentityUser entity)
        {
            UserManager _userManager = new UserManager();

            string userId = this.Get(entity.Id).ToString();

            ApplicationUser user = _userManager.FindById(userId);
            
            if (user == null)
            {
                //entity.Id = entity.Id;
                //entity.UserName = entity.UserName;
                //entity.Email = entity.Email;
                this.context.Users.Add(user);
            }
            else
            {
                //entity.Id = entity.Id;
                //entity.UserName = entity.UserName;
                //entity.Email = entity.Email;
                this.context.Entry(user)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }

    // UserManager deals with context calls for ApplicationUsers. Built into Identity Framework by default. 
    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        { }
    }
}