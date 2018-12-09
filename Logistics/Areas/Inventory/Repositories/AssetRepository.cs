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
    public class AssetRepository : IRepository<Asset>
    {
        InventoryContext context = new InventoryContext();

        public void Delete(Asset entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Asset>().Attach(entity);

            this.context.Assets.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Asset asset = this.Get(id);
            this.Delete(asset);
        }

        public IEnumerable<Asset> Find(Expression<Func<Asset, bool>> predicate)
        {
            return this.context.Assets
                .AsNoTracking()
                .Where(predicate);
        }

        public Asset Get(object id)
        {
            long assetId = long.Parse(id.ToString());
            return this.Find(asset => asset.Id == assetId)
                .SingleOrDefault();
        }

        public IEnumerable<Asset> GetAll()
        {
            return this.context.Assets;
        }

        public void Save(Asset entity)
        {
            Asset asset = this.Get(entity.Id);
            if (asset == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Assets.Add(entity);
            }
            else
            {
                entity.CreatedBy = asset.CreatedBy;
                entity.CreatedDate = asset.CreatedDate;
                asset = entity;
                asset.ModifiedDate = DateTime.Now;
                this.context.Entry(asset).State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}