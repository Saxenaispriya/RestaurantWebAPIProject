
using RestaurantWebAPIProject.Common.Exceptions;

namespace RestaurantWebAPIProject.Middleware
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid();
                _logger.LogError($"Error occure while processing the request, TraceId : {traceId}," +
                    $" Message : {ex.Message}, StackTrace: {ex.StackTrace}");

                context.Response.ContentType = "application/json";

                if (ex is NotFoundException)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }

                else if (ex is BadRequestException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }

                else
                {
                    context.Response.StatusCode=StatusCodes.Status500InternalServerError;
                }

                await context.Response.WriteAsJsonAsync(new
                {
                    message=ex.Message,
                    traceId=traceId,
                });
            }
        }
    }
}
