using Logistics.Helpers;
using Microsoft.AspNet.Identity;
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
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            try
            {
                //string userName = SessionHelper.SatisUser == null ? "System" : SessionHelper.SatisUser.UserName;
                string userName = User.Identity == null ? "System" : User.Identity.GetUserName();
                LogHelper.Save(exc, userName);
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
