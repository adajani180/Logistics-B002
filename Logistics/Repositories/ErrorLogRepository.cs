using Logistics.Entities.Error;
using Logistics.Modal;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class ErrorLogRepository : IRepository<ErrorLog>
    {
        public void Delete(ErrorLog entity)
        {
            try
            {
                using (var context = new ErrorLogContext())
                {
                    if (context.Entry(entity).State == EntityState.Detached)
                        context.Set<ErrorLog>().Attach(entity);

                    context.Errors.Remove(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e) { throw e; }
        }

        public void Delete(object id)
        {
            try
            {
                using (var context = new ErrorLogContext())
                {
                    ErrorLog error = this.Get(id);
                    this.Delete(error);
                }
            }
            catch (Exception e) { throw e; }
        }

        public IEnumerable<ErrorLog> Find(Expression<Func<ErrorLog, bool>> predicate)
        {
            IEnumerable<ErrorLog> errors = null;

            try
            {
                using (var context = new ErrorLogContext())
                {
                    errors = context.Errors
                        .AsNoTracking()
                        .Where(predicate);
                }
            }
            catch (Exception e) { throw e; }

            return errors;
        }

        public ErrorLog Get(object id)
        {
            ErrorLog error = null;

            try
            {
                using (var context = new ErrorLogContext())
                {
                    long errorId = long.Parse(id.ToString());
                    error = this.Find(err => err.Id == errorId)
                        .SingleOrDefault();
                }
            }
            catch (Exception e) { throw e; }

            return error;
        }

        public IEnumerable<ErrorLog> GetAll()
        {
            IEnumerable<ErrorLog> errors = null;

            try
            {
                using (var context = new ErrorLogContext())
                {
                    errors = context.Errors;
                }
            }
            catch (Exception e) { throw e; }

            return errors;
        }

        public void Save(ErrorLog entity)
        {
            try
            {
                using (var context = new ErrorLogContext())
                {
                    context.Errors.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e) { throw e; }
        }
    }
}