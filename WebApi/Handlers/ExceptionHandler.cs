using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Handlers;

public class ExceptionHandler: IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}