using Logistics.Entities.Appointment;
using Logistics.Models;
using Logistics.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Logistics.Repositories
{
    public class AppointmentScheduleRepository : IRepository<AppointmentSchedule>
    {
        AppointmentContext context = new AppointmentContext();

        public void Delete(AppointmentSchedule entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<AppointmentSchedule>().Attach(entity);

            this.context.AppointmentSchedules.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            AppointmentSchedule appSched = this.Get(id);
            this.Delete(appSched);
        }

        public IEnumerable<AppointmentSchedule> Find(Expression<Func<AppointmentSchedule, bool>> predicate)
        {
            return this.context.AppointmentSchedules
                .AsNoTracking()
                .Where(predicate);
        }

        public AppointmentSchedule Get(object id)
        {
            long appSchedId = long.Parse(id.ToString());
            return this.Find(appSched => appSched.Id == appSchedId)
                .SingleOrDefault();
        }

        public IEnumerable<AppointmentSchedule> GetAll()
        {
            return this.context.AppointmentSchedules;
        }

        public void Save(AppointmentSchedule entity)
        {
            AppointmentSchedule appSched = this.Get(entity.Id);
            if (appSched == null)
            {
                //entity.CreatedBy = entity.ModifiedBy;
                //entity.CreatedDate = DateTime.Now;
                //entity.ModifiedBy = null;
                this.context.AppointmentSchedules
                    .Add(entity);
            }
            else
            {
                //entity.CreatedBy = appSched.CreatedBy;
                //entity.CreatedDate = appSched.CreatedDate;
                appSched = entity;
                //appSched.ModifiedDate = DateTime.Now;                
                this.context.Entry(appSched)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}