using Logistics.Entities;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class LocationRepository : IRepository<Location>
    {
        LocationContext context = new LocationContext();

        public void Delete(Location entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Location>().Attach(entity);

            this.context.Locations.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Location location = this.Get(id);
            this.Delete(location);
        }

        public IEnumerable<Location> Find(Expression<Func<Location, bool>> predicate) => this.context.Locations.AsNoTracking().Where(predicate);

        public Location Get(object id)
        {
            long locId = long.Parse(id.ToString());
            return this.Find(loc => loc.Id == locId)
                .SingleOrDefault();
        }

        public IEnumerable<Location> GetAll() => this.context.Locations;

        public void Save(Location entity)
        {
            Location loc = this.Get(entity.Id);
            if (loc == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Locations
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = loc.CreatedBy;
                entity.CreatedDate = loc.CreatedDate;
                loc = entity;
                loc.ModifiedDate = DateTime.Now;
                this.context.Entry(loc)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}