using CqrsMediatorExamp.Exceptions;
using MediatR;
using MediatR.Pipeline;
using Newtonsoft.Json;
using System.Net;

namespace CqrsMediatorExamp.Helpers
{
    public class RequestGenericExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TRequest : IRequest<TResponse>
        where TException : Exception
    {
        private ILogger<RequestGenericExceptionHandler<TRequest, TResponse, TException>> _logger;
        private HttpContext _context;

        public RequestGenericExceptionHandler(
            ILogger<RequestGenericExceptionHandler<TRequest, TResponse, TException>> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            if (httpContextAccessor.HttpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }
            _context = httpContextAccessor.HttpContext;
        }

        public async Task Handle(TRequest request,
            TException exception,
            RequestExceptionHandlerState<TResponse> state,
            CancellationToken cancellationToken)
        {
            _logger.LogError($"Something went wrong", exception);
            await HandleExceptionAsync(_context, exception);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var title = "Internal error";
            var statusCode = HttpStatusCode.InternalServerError;
            string message;
            if (exception is ExceptionBase)
            {
                var exceptionBase = exception as ExceptionBase;
                if (exceptionBase != null)
                {
                    title = exceptionBase.Title;
                    statusCode = exceptionBase.StatusCode;
                }
                message = exception.Message;
                _logger.LogError(exception.Message, exception);
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = "De user has not permissions to access this page.";
                _logger.LogWarning(exception.Message, exception);
            }
            else
            {
                var mostInnerException = GetInnerException(exception);
                if (!mostInnerException.Equals(exception))
                {
                    _logger.LogError(mostInnerException.Message, mostInnerException);
                }
                _logger.LogError(exception.Message, exception);

                message = "Something went wrong. Please contact the admin of this application or see server logs.";
            }

            var result = new { title, statusCode, detail = message };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private static Exception GetInnerException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetInnerException(exception.InnerException);
            }

            return exception;
        }
    }
}
