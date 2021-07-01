using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IPriceDataService, PriceDataService>();
        }
    }
}
