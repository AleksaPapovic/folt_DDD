using System;
using System.Globalization;

namespace FoltDelivery.API.Exception
{
    public class AppException : System.Exception
    {
        public AppException()
        {
        }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
