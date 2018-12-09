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
    public class PhoneRepository : IRepository<Phone>
    {
        PersonnelContext context = new PersonnelContext();

        public void Delete(Phone entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Phone>().Attach(entity);

            this.context.Phones.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Phone phone = this.Get(id);
            this.Delete(phone);
        }

        public IEnumerable<Phone> Find(Expression<Func<Phone, bool>> predicate)
        {
            return this.context.Phones
                .Where(predicate);
        }

        public Phone Get(object id)
        {
            long phoneId = long.Parse(id.ToString());
            return this.Find(phone => phone.Id == phoneId)
                .SingleOrDefault();
        }

        public IEnumerable<Phone> GetAll()
        {
            return this.context.Phones;
        }

        public void Save(Phone entity)
        {
            Phone phone = this.Get(entity.Id);
            if (phone == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Phones
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = phone.CreatedBy;
                entity.CreatedDate = phone.CreatedDate;
                phone = entity;
                phone.ModifiedDate = DateTime.Now;
                this.context.Entry(phone)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}