namespace TicketManagement.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExecptionHandlerMiddleware>();
        }
    }
}
