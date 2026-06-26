using System.Net;

namespace GymManagementSystem.Application.Shared.Exceptions;

public class IntegrationException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public IntegrationException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
    {
        StatusCode = statusCode;
    }
}
