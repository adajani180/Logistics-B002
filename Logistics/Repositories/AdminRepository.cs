using Logistics.Entities.Access;
using Logistics.Modal;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class AdminRepository : IRepository<SystemUser>
    {
        AdminContext context = new AdminContext();

        public void Delete(SystemUser entity)
        {
            try
            {
                if (this.context.Entry(entity).State == EntityState.Detached)
                    this.context.Set<SystemUser>().Attach(entity);

                this.context.SystemUsers.Remove(entity);
                this.context.SaveChanges();
            }
            catch (Exception e) { throw e; }
        }

        public void Delete(object id)
        {
            try
            {
                SystemUser user = this.Get(id);
                this.Delete(user);
            }
            catch (Exception e) { throw e; }
        }

        public IEnumerable<SystemUser> Find(Expression<Func<SystemUser, bool>> predicate)
        {
            try
            {
                return this.context.SystemUsers
                    .AsNoTracking()
                    .Where(predicate);
            }
            catch (Exception e) { throw e; }
        }

        public SystemUser Get(object id)
        {
            try
            {
                Expression<Func<SystemUser, bool>> predicate = null;

                if (id is string)
                {
                    predicate = usr => usr.UserName.ToLower().Equals(id.ToString().ToLower());
                }
                else
                {
                    long sysUserId = long.Parse(id.ToString());
                    predicate = usr => usr.Id == sysUserId;
                }

                return this.Find(predicate).SingleOrDefault();
            }
            catch (Exception e) { throw e; }
        }

        public IEnumerable<SystemUser> GetAll()
        {
            try
            {
                return this.context.SystemUsers;
            }
            catch (Exception e) { throw e; }
        }

        public void Save(SystemUser entity)
        {
            try
            {
                SystemUser sysUser = this.Get(entity.Id);
                if (sysUser == null)
                {
                    this.context.SystemUsers.Add(entity);
                }
                else
                {
                    sysUser = entity;
                    this.context.Entry(sysUser).State = EntityState.Modified;
                }
                this.context.SaveChanges();
            }
            catch (Exception e) { throw e; }
        }
    }
}