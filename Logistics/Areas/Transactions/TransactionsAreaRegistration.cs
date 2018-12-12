using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Logistics.Areas.Transactions
{
    public class TransactionsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Transactions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Transactions_default",
                "Transactions/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}