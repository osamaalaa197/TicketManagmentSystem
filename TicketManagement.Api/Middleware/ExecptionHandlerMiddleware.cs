
using System.Net;
using TicketManagementSystem.Application.Exceptions;

namespace TicketManagement.Api.Middleware
{
    public class ExecptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExecptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }
        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode= HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result=string.Empty;
            switch (exception)
            {
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message });
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                        result=badRequestException.Message;
                    break;
                default:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }
            context.Response.StatusCode = (int)httpStatusCode;
            if (result==string.Empty)
            {
                result = System.Text.Json.JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}
