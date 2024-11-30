using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Occasia.Modules.Events.Presentation.ApiResults;

public static class ApiResults
{
    public static IResult ToResult<TResponse>(this ErrorOr<TResponse> result,
        Func<TResponse, IResult>? onSuccess = null)
    {
        onSuccess ??= Results.Ok;
        return result.Match(
            response => onSuccess(response),
            ProblemResult);
    }

    private static IResult ProblemResult(List<Error> errors)
    {
        return Results.Problem(ProblemDetails(errors));
    }

    private static ProblemDetails ProblemDetails(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return TypedResults.Problem().ProblemDetails;
        }

        if (errors.TrueForAll(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblemResult(errors).ProblemDetails;
        }

        return ProblemResult(errors[0]).ProblemDetails;
    }

    private static ProblemHttpResult ProblemResult(Error error)
    {
        return TypedResults.Problem(
            title: error.Code,
            detail: error.Description,
            type: GetType(),
            statusCode: GetStatusCode()
        );

        int GetStatusCode() => error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Failure or ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        string GetType() => error.Type switch
        {
            ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorType.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            ErrorType.Failure or ErrorType.Unexpected => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
    }

    private static ValidationProblem ValidationProblemResult(IEnumerable<Error> errors)
    {
        var validationErrors = errors
            .GroupBy(error => error.Code)
            .ToDictionary(
                group => group.Key,
                group => group.Select(error => error.Description).ToArray());

        return TypedResults.ValidationProblem(validationErrors);
    }
}
