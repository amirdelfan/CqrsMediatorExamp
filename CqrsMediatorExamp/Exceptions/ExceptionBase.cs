using System.Net;

namespace CqrsMediatorExamp.Exceptions
{
    public class ExceptionBase : Exception
    {
        public ExceptionBase()
        {
            Title = "";
        }

        public ExceptionBase(HttpStatusCode statusCode, string title)
        {
            StatusCode = statusCode;
            Title = title;
        }

        public ExceptionBase(string message)
            : base(message)
        {
            Title = "";
        }

        public ExceptionBase(string message, HttpStatusCode statusCode, string title)
            : base(message)
        {
            StatusCode = statusCode;
            Title = title;
        }

        public ExceptionBase(string message, HttpStatusCode statusCode, string title, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            Title = title;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Title { get; set; }
    }
}
