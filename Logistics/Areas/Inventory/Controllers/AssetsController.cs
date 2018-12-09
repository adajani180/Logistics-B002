using Logistics.Areas.Config.Repositories;
using Logistics.Areas.Inventory.Entities;
using Logistics.Areas.Inventory.Repositories;
using Logistics.Controllers;
using Logistics.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Logistics.Areas.Inventory.Controllers
{
    public class AssetsController : BaseController
    {
        #region Main

        private AssetRepository assetRepo;
        private BinRepository binRepo;
        private WarehouseRepository warehouseRepo;
        private LookupRepository lookupRepo;

        public AssetsController()
        {
            this.assetRepo = new AssetRepository();
            this.binRepo = new BinRepository();
            this.warehouseRepo = new WarehouseRepository();
            this.lookupRepo = new LookupRepository();
        }

        #endregion

        // GET: Inventory/Equipment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var modal = this.assetRepo.Get(id);

            ViewBag.ListOfWarehouses = this.GetWarehouses(modal?.WarehouseId);
            ViewBag.ListOfBins = this.GetBins(modal?.BinId);
            ViewBag.ListOfAssetTypes = this.GetAssetTypes(modal?.TypeLookupId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult Save(Asset asset)
        {
            asset.ModifiedBy = SessionHelper.SatisUser.Id;
            this.assetRepo.Save(asset);

            MessageHelper message = new MessageHelper(true, "The Asset was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            this.assetRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The Asset was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #region Functions

        private IEnumerable<SelectListItem> GetWarehouses(long? selectedId)
        {
            //this.warehouseRepo = new WarehouseRepository();
            return this.warehouseRepo.GetAll()
                .Select(warehouse => new SelectListItem
                {
                    Value = warehouse.Id.ToString(),
                    Text = warehouse.Name,
                    Selected = (warehouse.Id == selectedId)
                });
        }

        private IEnumerable<SelectListItem> GetBins(long? selectedId)
        {
            //this.binRepo = new BinRepository();
            return this.binRepo.GetAll()
                .Select(bin => new SelectListItem
                {
                    Value = bin.Id.ToString(),
                    Text = bin.Name,
                    Selected = (bin.Id == selectedId)
                });
        }

        private IEnumerable<SelectListItem> GetAssetTypes(long? selectedId)
        {
            return this.lookupRepo
                .Find(lookup => lookup.Type.ToLower().Equals("asset type"))
                .Select(lookup => new SelectListItem
                {
                    Value = lookup.Id.ToString(),
                    Text = lookup.Name,
                    Selected = (lookup.Id == selectedId)
                });
        }

        #endregion
    }
}