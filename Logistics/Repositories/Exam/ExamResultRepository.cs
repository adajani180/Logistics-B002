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
    public class ExamResultRepository : IRepository<ExamResult>
    {
        ExamContext context = new ExamContext();

        public void Delete(ExamResult entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<ExamResult>().Attach(entity);

            this.context.ExamResults.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            ExamResult result = this.Get(id);
            this.Delete(result);
        }

        public IEnumerable<ExamResult> Find(Expression<Func<ExamResult, bool>> predicate)
        {
            return this.context.ExamResults
                .AsNoTracking()
                .Where(predicate);
        }

        public ExamResult Get(object id)
        {
            Expression<Func<ExamResult, bool>> predicate = null;
            long resultId = long.Parse(id.ToString());
            predicate = result => result.Id == resultId;

            return this.Find(predicate)
                .SingleOrDefault();
        }

        public IEnumerable<ExamResult> GetAll()
        {
            return this.context.ExamResults;
        }

        public void Save(ExamResult entity)
        {
            ExamResult result = this.Get(entity.Id);
            if (result == null)
            {
                //entity.CreatedBy = entity.ModifiedBy;
                //entity.CreatedDate = DateTime.Now;
                //entity.ModifiedBy = null;
                this.context.ExamResults
                    .Add(entity);
            }
            else
            {
                //entity.CreatedBy = exam.CreatedBy;
                //entity.CreatedDate = exam.CreatedDate;
                result = entity;
                //exam.ModifiedDate = DateTime.Now;
                this.context.Entry(result)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}