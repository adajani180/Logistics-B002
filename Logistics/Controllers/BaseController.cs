using Logistics.Exceptions;
using Logistics.Helpers;
using System.Web.Mvc;

namespace Logistics.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
            //if (filterContext.ActionDescriptor.ActionName != "NotAuthorized" &&
            //    filterContext.ActionDescriptor.ActionName != "Error")
            //{
            //    AuthenticateUser(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            //}

            //if (User.Identity.IsAuthenticated)
            //{
            //    RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    RedirectToAction("Login", "Account");
            //}

        }

        //protected void AuthenticateUser()
        //{
        //    if (SessionHelper.SatisUser == null)
        //        throw new SessionExpiredException();
        //}
    }
}