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
    public class AppointmentRepository : IRepository<Appointment>
    {
        AppointmentContext context = new AppointmentContext();

        public void Delete(Appointment entity)
        {
            if (this.context.Entry(entity).State == EntityState.Detached)
                this.context.Set<Appointment>().Attach(entity);

            this.context.Appointments.Remove(entity);
            this.context.SaveChanges();
        }

        public void Delete(object id)
        {
            Appointment app = this.Get(id);
            this.Delete(app);
        }

        public IEnumerable<Appointment> Find(Expression<Func<Appointment, bool>> predicate)
        {
            return this.context.Appointments
                .AsNoTracking()
                .Where(predicate);
        }

        public Appointment Get(object id)
        {
            //DateTime appointmentDate;
            //if (DateTime.TryParse(id.ToString(), out appointmentDate))
            //{
            //    predicate = app => app.AppointmentDate == appointmentDate;
            //}
            //else
            //{
            //}

            long appointmentId = long.Parse(id.ToString());
            return this.Find(app => app.Id == appointmentId)
                .SingleOrDefault();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return this.context.Appointments;
        }

        public void Save(Appointment entity)
        {
            Appointment appointment = this.Get(entity.Id);
            if (appointment == null)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedBy = null;
                this.context.Appointments
                    .Add(entity);
            }
            else
            {
                entity.CreatedBy = appointment.CreatedBy;
                entity.CreatedDate = appointment.CreatedDate;
                appointment = entity;
                appointment.ModifiedDate = DateTime.Now;
                this.context.Entry(appointment)
                    .State = EntityState.Modified;
            }

            this.context.SaveChanges();
        }
    }
}