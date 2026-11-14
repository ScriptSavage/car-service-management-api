using Application.Exceptions;

namespace Api.Middlewares;

public class GlobalErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        catch (InvalidOperationException invalidOperationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (UnauthorizedAccessException unauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        catch (Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            throw;
        }
    }
}