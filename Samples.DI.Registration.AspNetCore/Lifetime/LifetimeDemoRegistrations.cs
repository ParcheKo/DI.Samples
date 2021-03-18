using Microsoft.Extensions.DependencyInjection;
using Samples.DI.Shared.Operation;

namespace Samples.DI.Registration.AspNetCore.Lifetime
{
    public static class LifetimeDemoRegistrations
    {
        public static IServiceCollection RegisterLifetimeDemoDependencies(this IServiceCollection services)
        {
            return services
                .AddTransient<ITransientOperation, DefaultOperation>()
                .AddSingleton<ISingletonOperation, DefaultOperation>()

                //// two scoped services with the same implementation which resolve to two different values in the same scope
                //.AddScoped<IScopedOperation, DefaultOperation>()
                //.AddScoped<IAnotherScopedOperation, DefaultOperation>()

                //// two scoped services with the same implementation which resolve to the same value in the same scope
                // Solution (better to name it a trick!) for type-forwarding in asp net DI
                .AddScoped<DefaultOperation>()
                .AddScoped<IScopedOperation>(sp => sp.GetRequiredService<DefaultOperation>())
                .AddScoped<IAnotherScopedOperation>(sp => sp.GetRequiredService<DefaultOperation>())

                //// Another solution for type-forwarding in asp net DI (works only for singletons)
                //.AddScoped<DefaultOperation>()
                //.AddScoped<ISingletonOperation>(sp => sameOperationInstanceForTwoDifferentServices)
                //.AddScoped<IAnotherSingletonOperation>(sp => sameOperationInstanceForTwoDifferentServices)

                .AddTransient<OperationLogger>();
        } 
    }
}
