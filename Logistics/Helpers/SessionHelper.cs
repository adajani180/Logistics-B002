using Logistics.Entities.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistics.Helpers
{
    public static class SessionHelper
    {
        public enum SessionEnum
        {
            UserInSession
        }

        public static SystemUser SatisUser
        {
            get { return (SystemUser)HttpContext.Current.Session[Enum.GetName(typeof(SessionEnum), SessionEnum.UserInSession)] ?? null; }
            set { HttpContext.Current.Session[Enum.GetName(typeof(SessionEnum), SessionEnum.UserInSession)] = value; }
        }
    }
}