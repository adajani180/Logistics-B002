using Logistics.Areas.Inventory.Entities;
using Logistics.Areas.Inventory.Repositories;
using Logistics.Controllers;
using Logistics.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Logistics.Areas.Inventory.Controllers
{
    public class BinsController : BaseController
    {
        private BinRepository binRepo;
        private WarehouseRepository warehouseRepo;

        public BinsController()
        {
            this.binRepo = new BinRepository();
        }

        // GET: Inventory/Bins
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.binRepo.Get(id);

            ViewBag.ListOfWarehouses = this.GetWarehouses(modal?.WarehouseId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(Bin bin)
        {
            bin.ModifiedBy = SessionHelper.SatisUser.Id;
            this.binRepo.Save(bin);

            MessageHelper message = new MessageHelper(true, "The Bin was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.binRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Bin was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<SelectListItem> GetWarehouses(long? selectedId)
        {
            this.warehouseRepo = new WarehouseRepository();
            return this.warehouseRepo.GetAll()
                .Select(warehouse => new SelectListItem
                {
                    Value = warehouse.Id.ToString(),
                    Text = warehouse.Name,
                    Selected = (warehouse.Id == selectedId)
                });
        }
    }
}