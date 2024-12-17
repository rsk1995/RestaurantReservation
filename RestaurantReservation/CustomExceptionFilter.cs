using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var errorResponse = new
        {
            Message = "An unexpected error occurred. Please try again later.",
            Detail = context.Exception.Message
        };

        // Log the exception (optional)
        // Log(context.Exception);

        context.Result = new JsonResult(errorResponse)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true; // Mark exception as handled
    }
}
