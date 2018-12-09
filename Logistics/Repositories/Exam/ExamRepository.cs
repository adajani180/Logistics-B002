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
    public class ExamRepository : IRepository<Exam>
    {
        ExamContext context = new ExamContext();

        public void Delete(Exam entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Exam>().Attach(entity);

            this.context.Exams.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Exam exam = this.Get(id);
            this.Delete(exam);
        }

        public IEnumerable<Exam> Find(Expression<Func<Exam, bool>> predicate)
        {
            return this.context.Exams
                .AsNoTracking()
                .Where(predicate);
        }

        public Exam Get(object id)
        {
            Expression<Func<Exam, bool>> predicate = null;
            long examId = long.Parse(id.ToString());
            predicate = exam => exam.Id == examId;

            return this.Find(predicate)
                .SingleOrDefault();
        }

        public IEnumerable<Exam> GetAll()
        {
            return this.context.Exams;
        }

        public void Save(Exam entity)
        {
            Exam exam = this.Get(entity.Id);
            if (exam == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Exams
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = exam.CreatedBy;
                entity.CreatedDate = exam.CreatedDate;
                exam = entity;
                exam.ModifiedDate = DateTime.Now;
                this.context.Entry(exam)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}