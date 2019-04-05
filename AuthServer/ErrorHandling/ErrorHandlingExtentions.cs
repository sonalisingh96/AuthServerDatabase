using Microsoft.AspNetCore.Builder;

namespace AuthServer.ErrorHandling
{
    public static class ErrorHandlingExtentions
    {
        public static IApplicationBuilder UseErrorHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}