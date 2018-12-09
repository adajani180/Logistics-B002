using Logistics.Areas.Inventory.Entities;
using Logistics.Areas.Inventory.Repositories;
using Logistics.Controllers;
using Logistics.Helpers;
using System.Web.Mvc;

namespace Logistics.Areas.Inventory.Controllers
{
    public class WarehouseController : BaseController
    {
        private WarehouseRepository repo;

        public WarehouseController()
        {
            this.repo = new WarehouseRepository();
        }

        // GET: Inventory/Warehouse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.repo.Get(id);
            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(Warehouse warehouse)
        {
            warehouse.ModifiedBy = SessionHelper.SatisUser.Id;
            this.repo.Save(warehouse);

            MessageHelper message = new MessageHelper(true, "The Warehouse was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.repo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Warehouse was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}