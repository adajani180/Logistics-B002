using Logistics.Entities.Access;
using Logistics.Helpers;
using Logistics.Repositories;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class AdminController : BaseController
    {
        #region Main

        private AdminRepository accessRepo;

        public AdminController()
        {
            this.accessRepo = new AdminRepository();
        }

        #endregion

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile(long id = 0)
        {
            var modal = this.accessRepo.Get(id);

            ViewBag.ListOfStatuses = MiscHelper.GetLookupSelectListItems("user status", modal?.StatusLookupId);

            return View(modal);
        }

        public ActionResult SaveUserProfile(SystemUser user)
        {
            this.accessRepo.Save(user);

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
        public ActionResult DeleteUser(long id)
        {
            this.accessRepo.Delete(id);

            MessageHelper message = new MessageHelper(true, "The User was deleted successfully.");
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}