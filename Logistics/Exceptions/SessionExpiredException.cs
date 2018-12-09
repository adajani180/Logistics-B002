using System;

namespace Logistics.Exceptions
{
    public class SessionExpiredException : Exception
    {
        private string _message = "Your session has expired.";

        public SessionExpiredException()
        {
        }

        public SessionExpiredException(string message)
            : base(message)
        {
            _message = message;
        }
    }
}