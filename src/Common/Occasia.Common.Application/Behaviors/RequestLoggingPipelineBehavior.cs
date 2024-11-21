using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Occasia.Common.Application.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string moduleName = GetModuleName(typeof(TRequest).FullName!);
        string requestName = typeof(TRequest).Name;

        using (LogContext.PushProperty("Module", moduleName))
        {
            logger.LogInformation("Processing request {RequestName}", requestName);

            TResponse result = await next();

            if (result.IsError)
            {
                using (LogContext.PushProperty("Errors", result.Errors, true))
                {
                    logger.LogError("Completed request {RequestName} with error", requestName);
                }
            }
            else
            {
                logger.LogInformation("Completed request {RequestName}", requestName);
            }

            return result;
        }
    }

    private static string GetModuleName(string requestName)
    {
        return requestName.Split('.')[2];
    }
}
