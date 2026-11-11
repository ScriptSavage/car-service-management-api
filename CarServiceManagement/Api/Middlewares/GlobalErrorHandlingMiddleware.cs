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
            Console.WriteLine(notFoundException.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;

        }
        catch (InvalidOperationException invalidOperationException)
        {
            Console.WriteLine(invalidOperationException.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (UnauthorizedAccessException unauthorizedAccessException)
        {
            Console.WriteLine(unauthorizedAccessException.Message);
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}