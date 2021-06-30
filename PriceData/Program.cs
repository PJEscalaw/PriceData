using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace PriceData
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var startup = new Startup();
            var scopeFactory = startup.ConfigureServices()
                                      .BuildServiceProvider()
                                      .GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            await services.GetService<PriceData>().RunAsync();
        }
    }
}
