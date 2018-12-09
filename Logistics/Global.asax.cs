using Logistics.Helpers;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Logistics
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // todo: do something with the version number
            this.GetAppVersion();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //string userName = this.User.Identity.Name;
            //userName = userName.Substring(userName.IndexOf("\\") + 1);

            //if (userName == null)
            //{
            //}
            //else
            //{
            //    AccessRepository accessRepo = new AccessRepository();
            //    SystemUser user = accessRepo.Get(userName);
            //    Session.Add(Enum.GetName(typeof(SessionEnum), SessionEnum.UserInSession), user);
            //}
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            try
            {
                string userName = SessionHelper.SatisUser == null ? "System" : SessionHelper.SatisUser.UserName;
                ErrorLogHelper.Save(exc, userName);
            }
            finally { }

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("error", exc);
            routeData.Values.Add("statusCode", 500);
        }

        /// <summary>
        /// Returns the application version from the web.config key
        /// </summary>
        /// <returns></returns>
        private string GetAppVersion()
        {
            return ConfigurationManager.AppSettings["AppVersion"].ToString();
        }
    }
}
