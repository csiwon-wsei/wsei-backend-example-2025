using ApplicationCore.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApi.Handlers;

public class ProblemDetailsExceptionHandler(
    ProblemDetailsFactory factory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is MovieNotFoundException)
        {
            var problem = factory.CreateProblemDetails(
                context,
                StatusCodes.Status404NotFound,
                "Error during adding new review",
                "Service error",
                detail: exception.Message
            );
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(problem);
            return true;
        }

        if (exception is UserNotFoundException)
        {
            var problem = factory.CreateProblemDetails(
                context,
                StatusCodes.Status400BadRequest,
                "Error during adding new review",
                "Service error",
                detail: exception.Message
            );
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problem);
            return true;
        }

        return false;
    }
}