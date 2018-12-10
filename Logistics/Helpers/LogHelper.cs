using Logistics.Entities.Error;
using Logistics.Repositories;
using NLog;
using System;

namespace Logistics.Helpers
{
    public class LogHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

            Log(LogLevel.Error, error.Message);
        }

        public static void Log(LogLevel level, string msg) => logger.Log(level, msg);
    }
}