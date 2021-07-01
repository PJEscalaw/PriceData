using Microsoft.AspNetCore.Builder;
using PriceData.WebAPI.Middlewares;

namespace PriceData.WebAPI.Extensions
{
    public static class AppExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
