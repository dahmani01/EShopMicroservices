using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handling request={RequestName} - Response={@Response}", typeof(TRequest).Name,
            typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = next();

        timer.Stop();
        var timeTaken = timer.Elapsed;

        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[Performance] {RequestName} took {ElapsedMilliseconds} ms to complete",
                typeof(TRequest).Name, timeTaken.TotalMilliseconds);
        }

        logger.LogInformation("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);

        return response;
    }
}