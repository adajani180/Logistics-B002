using Logistics.Entities;
using Logistics.Helpers;
using Logistics.Repositories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class ExamsController : BaseController
    {
        private ExamRepository examRepo;

        public ExamsController()
        {
            this.examRepo = new ExamRepository();
        }

        // GET: Exams
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.examRepo.Get(id);
            string expCycle = modal?.ExpirationCycle;

            ViewBag.ListOfExpirationCycles = this.GetExpirationCycles(expCycle);

            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(Exam exam)
        {
            exam.ModifiedBy = SessionHelper.SatisUser.Id;
            this.examRepo.Save(exam);

            MessageHelper message = new MessageHelper(true, "The Exam was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.examRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Exam was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetExpirationCycles(string selectedItem)
        {
            List<SelectListItem> cycles = new List<SelectListItem>();
            cycles.Add(new SelectListItem
            {
                Value = "Yearly",
                Text = "Yearly",
                Selected = ("Yearly" == selectedItem)
            });
            cycles.Add(new SelectListItem
            {
                Value = "2 Years",
                Text = "2 Years",
                Selected = ("2 Years" == selectedItem)
            });
            cycles.Add(new SelectListItem
            {
                Value = "Monthly",
                Text = "Monthly",
                Selected = ("Monthly" == selectedItem)
            });

            return cycles;
        }
    }
}