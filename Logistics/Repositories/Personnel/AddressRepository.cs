using Logistics.Entities.Contact;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        PersonnelContext context = new PersonnelContext();

        public void Delete(Address entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Address>().Attach(entity);

            this.context.Addresses.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Address address = this.Get(id);
            this.Delete(address);
        }

        public IEnumerable<Address> Find(Expression<Func<Address, bool>> predicate)
        {
            return this.context.Addresses
                .AsNoTracking()
                .Where(predicate);
        }

        public Address Get(object id)
        {
            long addressId = long.Parse(id.ToString());
            return this.Find(address => address.Id == addressId)
                .SingleOrDefault();
        }

        public IEnumerable<Address> GetAll()
        {
            return this.context.Addresses;
        }

        public void Save(Address entity)
        {
            Address address = this.Get(entity.Id);
            if (address == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Addresses
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = address.CreatedBy;
                entity.CreatedDate = address.CreatedDate;
                address = entity;
                address.ModifiedDate = DateTime.Now;
                this.context.Entry(address)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}