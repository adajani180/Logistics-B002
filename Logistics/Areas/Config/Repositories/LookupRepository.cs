using Logistics.Areas.Config.Entities;
using Logistics.Areas.Config.Models;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Areas.Config.Repositories
{
    public class LookupRepository : IRepository<ConfigLookup>
    {
        ConfigLookupContext context = new ConfigLookupContext();

        public void Delete(ConfigLookup entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<ConfigLookup>().Attach(entity);

            this.context.ConfigLookups.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            ConfigLookup lookup = this.Get(id);
            this.Delete(lookup);
        }

        public IEnumerable<ConfigLookup> Find(Expression<Func<ConfigLookup, bool>> predicate)
        {
            return this.context.ConfigLookups
                .Where(predicate);
        }

        public ConfigLookup Get(object id)
        {
            long lookupId = long.Parse(id.ToString());
            return this.context.ConfigLookups
                .AsNoTracking()
                .Where(lookup => lookup.Id == lookupId)
                .SingleOrDefault();
        }

        public IEnumerable<ConfigLookup> GetAll()
        {
            return this.context.ConfigLookups;
        }

        public void Save(ConfigLookup entity)
        {
            ConfigLookup lookup = this.Get(entity.Id);
            if (lookup == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.ConfigLookups
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = lookup.CreatedBy;
                entity.CreatedDate = lookup.CreatedDate;
                lookup = entity;
                lookup.ModifiedDate = DateTime.Now;
                this.context.Entry(lookup)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}