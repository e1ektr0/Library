using System.Net;

namespace Library.Exceptions;

public class PortalException : Exception
{
    public PortalException(string message, HttpStatusCode statusCode, Exception? ex = null,
        string? field = null, PortalExceptionCode code = PortalExceptionCode.None) : base(message, ex)
    {
        StatusCode = statusCode;
        Field = field;
        Code = code;
    }

    public HttpStatusCode StatusCode { get; }
    public string? Field { get; }
    public PortalExceptionCode Code { get; }
}