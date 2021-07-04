using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Persistence.Contexts;
using PriceData.WebApi;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
               w.EnvironmentName == "Development" &&
               w.ApplicationName == "PriceData.WebApi"));

            services.AddLogging();
            services.AddSingleton(_configuration);

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            EnsureDatabase();

        }
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<AppDbContext>();

            string dbPath = AppDomain.CurrentDomain.BaseDirectory;

            context.Database.SetConnectionString($"Data Source={dbPath}\\PriceData.db");

        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            
        }
    }
}
