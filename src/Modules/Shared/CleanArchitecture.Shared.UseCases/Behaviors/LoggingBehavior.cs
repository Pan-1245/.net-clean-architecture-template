using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Shared.UseCases.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Request Handling: { name } {@request }", typeof(TRequest).Name, JsonSerializer.Serialize(request));
        var response = await next();
        _logger.LogInformation("CleanArchitecture Response Handling: { name } {@response }", typeof(TResponse).Name, JsonSerializer.Serialize(response));

        return response;
    }
}