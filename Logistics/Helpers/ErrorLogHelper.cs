using Logistics.Entities.Error;
using Logistics.Repositories;
using System;

namespace Logistics.Helpers
{
    public class ErrorLogHelper
    {
        public static void Save(Exception exc, string userName)
        {
            if (exc == null) return;

            ErrorLog error = new ErrorLog()
            {
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                Message = exc.Message,
                StackTrace = exc.StackTrace
            };

            ErrorLogRepository repo = new ErrorLogRepository();
            repo.Save(error);
        }
    }
}