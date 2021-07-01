using Microsoft.AspNetCore.Builder;
using PriceData.WebApi.Middlewares;

namespace PriceData.WebApi.Extensions
{
    public static class AppExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
