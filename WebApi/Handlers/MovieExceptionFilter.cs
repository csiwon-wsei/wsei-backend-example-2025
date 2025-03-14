using ApplicationCore.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Handlers;

public class MovieExceptionFilter(): ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is MovieNotFoundException || context.Exception is UserNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new
            {
                title = context.Exception.Message
            });
            context.ExceptionHandled = true;   
        }
    }

    public async override Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is MovieNotFoundException || context.Exception is UserNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new
            {
                title = "Error during adding new review",
                error = context.Exception.Message
                
            });
            context.ExceptionHandled = true;   
        }
    }
}