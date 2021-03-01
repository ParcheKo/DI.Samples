using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace DI.Samples.CastleWindsor.ConsoleApp.Lifetime
{
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,
            IConfigurationStore store)
        {
            container.Register(Component
                // forwarding types to the same implementation : method 1
                .For<ISingletonOperation, IScopedOperation, IAnotherScopedOperation, ITransientOperation>()

                //forwarding types to the same implementation : method 2
                //.For<ISingletonOperation>()
                //.Forward<IScopedOperation>()
                //.Forward<IAnotherScopedOperation>()
                //.Forward<ITransientOperation>()
                .ImplementedBy<DefaultOperation>().LifestyleScoped());

            container.Register(Component
                .For<OperationLogger>().LifestyleTransient());
        }
    }
}
