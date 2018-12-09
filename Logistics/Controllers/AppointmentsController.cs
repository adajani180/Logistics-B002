using Logistics.Areas.Config.Entities;
using Logistics.Areas.Config.Repositories;
using Logistics.Entities;
using Logistics.Entities.Appointment;
using Logistics.Helpers;
using Logistics.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class AppointmentsController : BaseController
    {
        private AppointmentRepository appRepo;
        private AppointmentScheduleRepository appSchedRepo;
        private ExamRepository examRepo;
        private ExamResultRepository resultRepo;
        private LookupRepository lookupRepo;

        private string appointmentStatusType = "Appointment Status";
        private string resultStatusType = "Result";

        public AppointmentsController()
        {
            this.appRepo = new AppointmentRepository();
            this.appSchedRepo = new AppointmentScheduleRepository();
            this.examRepo = new ExamRepository();
            this.resultRepo = new ExamResultRepository();
            this.lookupRepo = new LookupRepository();
        }

        // GET: Appointments
        public ActionResult Index()
        {
            ViewBag.ListOfExams = this.GetExams();

            return View();
        }

        public ActionResult Details(string d)
        {
            ViewBag.AppointmentDate = DateTime.Parse(d).ToString("dddd, MMMM d, yyyy");
            return View();
            //AppointmentSchedule appSched = new AppointmentSchedule();
            //if (id != 0)
            //{
            //    appSched = this.appSchedRepo.Get(id);
            //}
            //else
            //{
            //    Appointment app = this.appRepo.Get(d);
            //    appSched.Appointment = new Appointment { AppointmentDate = DateTime.Parse(d) };
            //    appSched.AppointmentTime = TimeSpan.FromHours(DateTime.Now.Hour);
            //}
            //return View(appSched);
        }

        [HttpPost]
        public ActionResult Get(long id)
        {
            Appointment app = this.appRepo.Get(id);
            return Json(app, JsonRequestBehavior.AllowGet); ;
        }

        [HttpPost]
        public ActionResult Save(Appointment appointment)
        {
            appointment.ModifiedBy = SessionHelper.SatisUser.Id;
            this.appRepo.Save(appointment);

            MessageHelper message = new MessageHelper(true, "The Appointment was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.appRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Appointment was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #region Calendar

        [HttpPost]
        public ActionResult GetAll(string start, string end)
        {
            DateTime dtStart = DateTime.Parse(start);
            DateTime dtEnd = DateTime.Parse(end);
            Expression<Func<Appointment, bool>> predicate = app => app.AppointmentDate >= dtStart && app.AppointmentDate <= dtEnd;

            List<CalendarEvent> events = new List<CalendarEvent>();
            List<Appointment> apps = this.appRepo.Find(predicate).ToList();
            foreach (Appointment app in apps)
            {
                CalendarEvent calEvent = new CalendarEvent
                {
                    AppointmentId = app.Id,
                    AppointmentDate = app.AppointmentDate,
                    AppointmentTime = app.AppointmentTime,
                    Location = app.Location,
                    Notes = app.Notes
                };

                AppointmentSchedule sched = this.appSchedRepo
                    .Find(appSched => appSched.AppointmentId == app.Id)
                    .ToList()
                    .SingleOrDefault();
                if (sched != null)
                {
                    calEvent.AppointmentScheduleId = sched.Id;
                    calEvent.StatusId = sched.StatusLookupId ?? 0;
                    calEvent.StatusDescription = sched.StatusLookup?.Name;
                    calEvent.PersonId = sched.PersonId ?? 0;
                    calEvent.PersonFullName = sched.Person?.FullName;
                    calEvent.ExamId = sched.ExamId ?? 0;
                    calEvent.Notes = sched.Notes;
                }

                events.Add(calEvent);
            }

            return Json(events, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Schedule(AppointmentSchedule sched)
        {
            sched.StatusLookupId = this.GetStatusLookupId(this.appointmentStatusType, "Scheduled");
            this.appSchedRepo.Save(sched);

            MessageHelper message = new MessageHelper(true, "The Appointment was scheduled successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Confirm(long id)
        {
            AppointmentSchedule appSched = this.appSchedRepo.Get(id);
            appSched.StatusLookupId = this.GetStatusLookupId(this.appointmentStatusType, "Confirmed");
            this.appSchedRepo.Save(appSched);

            ExamResult result = new ExamResult()
            {
                ExamId = (long)appSched.ExamId,
                ExamDate = appSched.Appointment.AppointmentDate,
                PersonId = appSched.PersonId,
                ResultLookupId = this.GetStatusLookupId(this.resultStatusType, "Pending")
            };
            this.resultRepo.Save(result);

            MessageHelper message = new MessageHelper(true, "The Appointment was confirmed successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cancel(long id)
        {
            AppointmentSchedule appSched = this.appSchedRepo.Get(id);

            ExamResult result = this.resultRepo
                .Find(res => res.PersonId == appSched.PersonId && res.ExamId == appSched.ExamId && res.ExamDate == appSched.Appointment.AppointmentDate)
                .SingleOrDefault();
            if (result != null) this.resultRepo.Delete(result);

            appSched.PersonId = null;
            appSched.StatusLookupId = this.GetStatusLookupId(this.appointmentStatusType, "Available");
            appSched.ExamId = null;
            this.appSchedRepo.Save(appSched);            

            MessageHelper message = new MessageHelper(true, "The Appointment was canceled and made available successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Lookups

        private IEnumerable<SelectListItem> GetExams()
        {
            return this.examRepo.GetAll()
                .Select(exam => new SelectListItem
                {
                    Value = exam.Id.ToString(),
                    Text = exam.Name
                });
        }

        private long GetStatusLookupId(string type, string status)
        {
            ConfigLookup lookup = this.lookupRepo
                .Find(a => a.Type.Equals(type) && a.Name.Equals(status))
                .SingleOrDefault();
            return lookup.Id;
        }

        #endregion
    }
}