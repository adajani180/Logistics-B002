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
    public class EmailRepository : IRepository<EmailAddress>
    {
        PersonnelContext context = new PersonnelContext();

        public void Delete(EmailAddress entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<EmailAddress>().Attach(entity);

            this.context.Emails.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            EmailAddress email = this.Get(id);
            this.Delete(email);
        }

        public IEnumerable<EmailAddress> Find(Expression<Func<EmailAddress, bool>> predicate)
        {
            return this.context.Emails
                .AsNoTracking()
                .Where(predicate);
        }

        public EmailAddress Get(object id)
        {
            long emailId = long.Parse(id.ToString());
            return this.Find(email => email.Id == emailId)
                .SingleOrDefault();
        }

        public IEnumerable<EmailAddress> GetAll()
        {
            return this.context.Emails;
        }

        public void Save(EmailAddress entity)
        {
            EmailAddress email = this.Get(entity.Id);
            if (email == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Emails
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = email.CreatedBy;
                entity.CreatedDate = email.CreatedDate;
                email = entity;
                email.ModifiedDate = DateTime.Now;
                this.context.Entry(email)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}