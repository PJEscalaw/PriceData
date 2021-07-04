using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class PerformanceLoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger _logger;

        public PerformanceLoggingBehaviour(ILogger logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var name = typeof(TRequest).Name;

            _logger.Information($"Handling {name}.");

            _timer.Start();
            var response = await next();
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 3000)
            {
                _logger.Warning("Long Running Request: {name} ({ElapsedMilliseconds} milliseconds).",
                    name, _timer.ElapsedMilliseconds, request);

                return response;
            }

            return response;
        }
    }
}
