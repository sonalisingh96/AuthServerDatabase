using System;

namespace AuthServer.ErrorHandling
{
    public class AppException : ApplicationException
    {
        public int ErrorCode { get; }

        public AppException(int errorCode, string errorMessage) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }
    }
}