using Microsoft.Extensions.DependencyInjection;
using Shared;
using Application;

namespace PriceData
{
    public class Startup
    {
        public IServiceCollection ConfigureServices() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddInfrastructureShared();
            services.AddApplication();
            services.AddTransient<PriceData>();

            return services;
        }
    }
}
