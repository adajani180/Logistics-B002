using Logistics.Areas.Config.Models;
using Logistics.Areas.Inventory.Entities;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Areas.Inventory.Repositories
{
    public class BinRepository : IRepository<Bin>
    {
        InventoryContext context = new InventoryContext();

        public void Delete(Bin entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Bin>().Attach(entity);

            this.context.Bins.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Bin bin = this.Get(id);
            this.Delete(bin);
        }

        public IEnumerable<Bin> Find(Expression<Func<Bin, bool>> predicate)
        {
            return this.context.Bins
                .AsNoTracking()
                .Where(predicate);
        }

        public Bin Get(object id)
        {
            long whId = long.Parse(id.ToString());
            return this.Find(bin => bin.Id == whId)
                .SingleOrDefault();
        }

        public IEnumerable<Bin> GetAll()
        {
            return this.context.Bins;
        }

        public void Save(Bin entity)
        {
            Bin bin = this.Get(entity.Id);
            if (bin == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Bins.Add(entity);
            }
            else
            {
                entity.CreatedBy = bin.CreatedBy;
                entity.CreatedDate = bin.CreatedDate;
                bin = entity;
                bin.ModifiedDate = DateTime.Now;
                this.context.Entry(bin).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}