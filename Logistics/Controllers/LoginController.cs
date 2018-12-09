using Logistics.Entities.Access;
using Logistics.Helpers;
using Logistics.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class LoginController : Controller
    {
        #region Main

        private AdminRepository accessRepo;

        public LoginController()
        {
            this.accessRepo = new AdminRepository();
        }

        #endregion

        // GET: Login
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public ActionResult Index(SystemUser userLogin)
    //    {
    //        SystemUser user = this.accessRepo
    //            .Find(u => u.UserName.Equals(userLogin.UserName) && u.PasswordHash.Equals(userLogin.PasswordHash))
    //            .SingleOrDefault();
    //        if (user != null)
    //        {
    //            //FormsAuthentication.SetAuthCookie(user.UserName, false);
    //            //Session.Add(Enum.GetName(typeof(SessionEnum), SessionEnum.UserInSession), user);
    //            SessionHelper.SatisUser = user;
    //            return RedirectToAction("Index", "Home");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError("Error", "Login data is incorrect!");                
    //            return View(user);
    //        }
    //    }
    }
}