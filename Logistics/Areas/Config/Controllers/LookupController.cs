using Logistics.Areas.Config.Entities;
using Logistics.Areas.Config.Repositories;
using Logistics.Controllers;
using Logistics.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Logistics.Areas.Config.Controllers
{
    public class LookupController : BaseController
    {
        #region Main

        private LookupRepository lookupRepo;

        public LookupController()
        {
            this.lookupRepo = new LookupRepository();
        }

        #endregion

        // GET: Config/Lookup
        public ActionResult Index()
        {
            ViewBag.ListOfLookupTypes = this.GetLookupTypes();

            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.lookupRepo.Get(id);
            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(ConfigLookup lookup)
        {
            lookup.ModifiedBy = SessionHelper.SatisUser.Id;
            this.lookupRepo.Save(lookup);

            MessageHelper message = new MessageHelper(true, "The Lookup was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.lookupRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Lookup was deleted successfully.");
            return Json(new { Message = message, LookupTypes = this.GetLookupTypes() }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetLookupTypes() => this.lookupRepo.GetAll()
                .OrderBy(lookup => lookup.Type)
                //.Select(lookup => lookup.Type)
                .Distinct()
                .Select(lookup => new SelectListItem
                {
                    Value = lookup.Id.ToString(),
                    Text = lookup.Name
                });
    }
}