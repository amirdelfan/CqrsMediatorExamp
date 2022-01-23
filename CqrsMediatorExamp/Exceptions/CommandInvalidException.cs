using System.Net;

namespace CqrsMediatorExamp.Exceptions
{
    public class CommandInvalidException : ExceptionBase
    {
        public CommandInvalidException(string message)
            : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Title = "Command is ongeldig";
        }
    }
}
