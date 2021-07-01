using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructurePersistence(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
        }
    }
}
