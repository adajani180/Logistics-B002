using Logistics.Areas.Config.Entities;
using Logistics.Areas.Config.Repositories;
using Logistics.Entities.Access;
using Logistics.Helpers;
using Logistics.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class AccessController : Controller
    {
        private AccessRepository accessRepo;
        private LookupRepository lookupRepo;

        public AccessController()
        {
            this.accessRepo = new AccessRepository();
            this.lookupRepo = new LookupRepository();
        }

        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SystemUserForm(long id = 0)
        {
            var modal = this.accessRepo.Get(id);

            List<ConfigLookup> lookups = this.lookupRepo
                .Find(lookup => lookup.Type.ToLower().Equals("user status"))
                .ToList<ConfigLookup>();
            ViewData["LookupTypes"] = new SelectList(lookups, "Id", "Name", modal?.StatusLookupId);

            return View(modal);
        }

        [HttpPost]
        public ActionResult SystemUserForm(SystemUser systemUser)
        {
            this.accessRepo.Save(systemUser);

            MessageHelper message = new MessageHelper(true, "The User was saved successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);

            //if (ModelState.IsValid)
            //{
            //    //usr.ModifiedBy = this.user.Id;
            //    this.accessRepo.Save(systemUser);
            //    TempData["Message"] = "The User was saved successfully.";
            //    return RedirectToAction("Index");
            //}

            //// get one error at a time
            //ViewBag.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).First();
            //ViewData["LookupTypes"] = this.GetLookupTypes(systemUser);
            ////return RedirectToAction("SystemUserForm", systemUser.Id);
            //return View(systemUser);

            //// Below worked but no list refresh
            ////return RedirectToAction("LoadLookups");
            ////var model = this.lookupRepo.GetAll();
            ////return PartialView("_LookupList", model.ToList<ConfigLookup>());
            ////return RedirectToAction("Index");
            ////return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSystemUser(long id)
        {
            this.accessRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The User was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}