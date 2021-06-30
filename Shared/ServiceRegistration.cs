using Microsoft.Extensions.DependencyInjection;
using Shared.Services;

namespace Shared
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureShared(this IServiceCollection services) 
        {
            services.AddTransient<CsvService>();
        }
    }
}
