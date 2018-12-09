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
    public class WarehouseRepository : IRepository<Warehouse>
    {
        InventoryContext context = new InventoryContext();

        public void Delete(Warehouse entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Warehouse>().Attach(entity);

            this.context.Warehouses.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Warehouse wh = this.Get(id);
            this.Delete(wh);
        }

        public IEnumerable<Warehouse> Find(Expression<Func<Warehouse, bool>> predicate)
        {
            return this.context.Warehouses
                .AsNoTracking()
                .Where(predicate);
        }

        public Warehouse Get(object id)
        {
            long whId = long.Parse(id.ToString());
            return this.Find(wh => wh.Id == whId)
                .SingleOrDefault();
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return this.context.Warehouses;
        }

        public void Save(Warehouse entity)
        {
            Warehouse wh = this.Get(entity.Id);
            if (wh == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Warehouses.Add(entity);
            }
            else
            {
                entity.CreatedBy = wh.CreatedBy;
                entity.CreatedDate = wh.CreatedDate;
                wh = entity;
                wh.ModifiedDate = DateTime.Now;
                this.context.Entry(wh).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}