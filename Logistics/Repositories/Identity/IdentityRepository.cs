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

        // User Manager and Role Manager
        RoleManager _roleManager = new RoleManager();
        UserManager _userManager = new UserManager();

        // Find
        public IEnumerable<IdentityUser> Find(Expression<Func<IdentityUser, bool>> predicate) => this.context.Users.AsNoTracking().Where(predicate);

        // Get Users Current Role
        public IdentityRole GetRole(object id)
        {
            string userId = id.ToString();
            IdentityRole userRole = _roleManager.FindById(userId);

            return userRole;
        }

        // Get All Roles in the Database - returns a List
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        // Get User
        public IdentityUser Get(object id)
        {
            string userId = id.ToString();
            ApplicationUser user = _userManager.FindById(userId);

            return user;
        }

        // Get All Users - returns a List
        public IEnumerable<IdentityUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        // Saves changes to the user or creates new user
        public void Save(IdentityUser entity)
        {
            string userId = this.Get(entity.Id).ToString();

            ApplicationUser user = _userManager.FindById(userId);
            
            if (user == null)
            {
                this.context.Users.Add(user);
            }
            else
            {
                this.context.Entry(user).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }

        public void SaveRole(string userId, string oldRole, string newRole)
        {
            ApplicationUser user = _userManager.FindById(userId);

            if (oldRole != newRole)
            {
                _userManager.RemoveFromRole(user.Id, oldRole);
                _userManager.AddToRole(user.Id, newRole);
                this.context.SaveChanges();
            }
        }

        // Deletes a User if passed an Identity User Object
        public void Delete(IdentityUser entity)
        {
            string userId = this.Get(entity.Id).ToString();
            ApplicationUser user = _userManager.FindById(userId);

            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<IdentityUser>().Attach(entity);

            this.context.Users.Remove(user);
            this.context.SaveChanges();
        }

        // Deletes and User if passed and id or other partial object.
        public void Delete(object id)
        {
            string userId = id.ToString();
            ApplicationUser user = _userManager.FindById(userId);

            this.Delete(user);
        }
    }

    // UserManager And Role Manager Classes deal with context calls for ApplicationUsers. Built into Identity Framework by default. 
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