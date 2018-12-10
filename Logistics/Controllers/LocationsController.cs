using Logistics.Entities;
using Logistics.Helpers;
using Logistics.Repositories;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class LocationsController : BaseController
    {
        private LocationRepository locRepo;

        public LocationsController()
        {
            this.locRepo = new LocationRepository();
        }

        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.locRepo.Get(id);
            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(Location location)
        {
            long oldId = location.Id;
            location.ModifiedBy = SessionHelper.SatisUser.Id;
            this.locRepo.Save(location);
            
            MessageHelper message = new MessageHelper(true, "The Location was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.locRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Location was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}