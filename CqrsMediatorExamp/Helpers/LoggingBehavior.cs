using System.Diagnostics;
using System.Text.Json;
using MediatR;

namespace CqrsMediatorExamp.Helpers
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();

            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            string? currentUser = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(currentUser))
            {
                _logger.LogInformation($"{requestNameWithGuid} [{currentUser}]");
            }
            else
            {
                _logger.LogInformation($"{requestNameWithGuid}");
            }

            TResponse response;

            try
            {
                try
                {
                    _logger.LogDebug($"[PROPS] {requestNameWithGuid} {JsonSerializer.Serialize(request)}");
                }
                catch (NotSupportedException e)
                {
                    _logger.LogError($"[Serialization ERROR] {requestNameWithGuid} Could not serialize the request.", e);
                }

                response = await next();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug(
                    $"[END] {requestNameWithGuid}; Execution time={stopwatch.ElapsedMilliseconds}ms");
            }

            return response;
        }
    }
}
