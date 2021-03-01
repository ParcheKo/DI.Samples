using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace DI.Samples.AspNetCore.ConsoleApp.Lifetime
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
            var sameOperationInstanceForTwoDifferentServices = new DefaultOperation();
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services
                        .AddTransient<ITransientOperation, DefaultOperation>()
                        .AddSingleton<ISingletonOperation, DefaultOperation>()

                        //// two scoped services with the same implementation which resolve to two different values in the same scope
                        //.AddScoped<IScopedOperation, DefaultOperation>()
                        //.AddScoped<IAnotherScopedOperation, DefaultOperation>()

                        //// two scoped services with the same implementation which resolve to the same value in the same scope
                        // Thumbs up! Solution (better to name it a trick!) for type-forwarding in asp net DI
                        .AddScoped<DefaultOperation>()
                        .AddScoped<IScopedOperation>(sp => sp.GetRequiredService<DefaultOperation>())
                        .AddScoped<IAnotherScopedOperation>(sp => sp.GetRequiredService<DefaultOperation>())

                        //// Another solution for type-forwarding in asp net DI (works only in for singletons)
                        //.AddScoped<DefaultOperation>()
                        //.AddScoped<ISingletonOperation>(sp => sameOperationInstanceForTwoDifferentServices)
                        //.AddScoped<IAnotherSingletonOperation>(sp => sameOperationInstanceForTwoDifferentServices)

                        .AddTransient<OperationLogger>());
        }

        static void ExemplifyScoping(IServiceProvider services, string scope)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            OperationLogger logger = provider.GetRequiredService<OperationLogger>();
            logger.LogOperations($"{scope}-Call 1 .GetRequiredService<OperationLogger>()");

            Console.WriteLine("...");

            logger = provider.GetRequiredService<OperationLogger>();
            logger.LogOperations($"{scope}-Call 2 .GetRequiredService<OperationLogger>()");
        }
    }
}
