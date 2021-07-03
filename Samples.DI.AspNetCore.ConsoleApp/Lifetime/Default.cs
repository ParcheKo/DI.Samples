using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samples.DI.Shared.Operation;
using System;
using System.Threading.Tasks;
using Samples.DI.Registration.AspNetCore.Lifetime;

namespace Samples.DI.AspNetCore.ConsoleApp.Lifetime
{
    class Default
    {
        public static async Task DemoAsync(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            ExemplifyScoping(host.Services, "Scope 1");
            Console.WriteLine(".......................");
            ExemplifyScoping(host.Services, "Scope 2");

            await host.RunAsync();
        }


        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => services.RegisterLifetimeDemoDependencies());
        }

        static void ExemplifyScoping(IServiceProvider serviceProvider, string scope)
        {
            using var serviceScope = serviceProvider.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            OperationLogger logger = provider.GetRequiredService<OperationLogger>();
            logger.LogOperations($"{scope}-Call 1 to .GetRequiredService<OperationLogger>()");

            Console.WriteLine("...");

            logger = provider.GetRequiredService<OperationLogger>();
            logger.LogOperations($"{scope}-Call 2 to .GetRequiredService<OperationLogger>()");
        }
    }
}
