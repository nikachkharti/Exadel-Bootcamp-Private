using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ReportHub.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[START] Handle request = {Request} - Response = {Resposne} - RequestData = {RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var respone = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;

            //If the request is greater than 3 seconds, then log a warning.
            if (timeTaken.Seconds > 3)
            {
                _logger.LogWarning("[PERFORMANCE] The request = {Request} took {TimeTaken}", typeof(TRequest).Name, timeTaken);
            }

            _logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
            return respone;
        }
    }
}
