using System.Net;

namespace CleanArchitecture.Utilities.Exceptions;

public class GeneralException : BaseCustomException
{
    public GeneralException(string message, HttpStatusCode statusCode) : base(message, "GFE", statusCode) { }

    public class NotFound : GeneralException
    {
        public NotFound(string message) : base(message, HttpStatusCode.NotFound) { }
    }
}