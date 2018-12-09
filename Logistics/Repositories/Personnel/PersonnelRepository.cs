using Logistics.Entities.Personnel;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class PersonnelRepository : IRepository<Person>
    {
        PersonnelContext context = new PersonnelContext();

        public void Delete(Person entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Person>().Attach(entity);

            this.context.Personnel.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Person person = this.Get(id);
            this.Delete(person);
        }

        public IEnumerable<Person> Find(Expression<Func<Person, bool>> predicate)
        {
            return this.context.Personnel
                .AsNoTracking()
                .Where(predicate);
        }

        public Person Get(object id)
        {
            long perId = long.Parse(id.ToString());
            return this.Find(per => per.Id == perId)
                .SingleOrDefault();
        }

        public IEnumerable<Person> GetAll()
        {
            return this.context.Personnel;
        }

        public void Save(Person entity)
        {
            Person per = this.Get(entity.Id);
            if (per == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Personnel
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = per.CreatedBy;
                entity.CreatedDate = per.CreatedDate;
                per = entity;
                per.ModifiedDate = DateTime.Now;
                this.context.Entry(per)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}