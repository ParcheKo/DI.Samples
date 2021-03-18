using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Microsoft.Extensions.Hosting;
using Samples.DI.Shared.Operation;
using System;
using System.Threading.Tasks;
using Samples.DI.Registration.CastleWindsor.Lifetime;

namespace Samples.DI.CastleWindsor.ConsoleApp.Lifetime
{
    class Default
    {
        public static async Task LifetimeDemoAsync(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            var container = new WindsorContainer();
            container.Install(new LifetimeDemoDependenciesInstaller());

            ExemplifyScoping(container, "Scope 1");
            Console.WriteLine(".......................");
            ExemplifyScoping(container, "Scope 2");

            await host.RunAsync();
        }


        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);

        private static void ExemplifyScoping(IWindsorContainer container, string scope)
        {
            using IDisposable _ = container.BeginScope();

            OperationLogger logger = container.Resolve<OperationLogger>();
            logger.LogOperations($"{scope}-Call 1 to GetRequiredService<OperationLogger>()");

            Console.WriteLine("...");

            logger = container.Resolve<OperationLogger>();
            logger.LogOperations($"{scope}-Call 2 to GetRequiredService<OperationLogger>()");
        }
    }
}
