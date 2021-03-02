﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace DI.Samples.CastleWindsor.ConsoleApp.Lifetime
{
    public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container,
            IConfigurationStore store)
        {
            //// one singleton service
            container.Register(Component
                .For<ISingletonOperation>().Named("DefaultOperation-Singleton")
                .ImplementedBy<DefaultOperation>().LifestyleSingleton());

            //// two scoped services with the same implementation which resolve to tqo different values in the same scope
            //container.Register(Component
            //    .For<IAnotherScopedOperation>().Named("DefaultOperation-Scoped-1")
            //    .ImplementedBy<DefaultOperation>().LifestyleScoped());
            //container.Register(Component
            //    .For<IScopedOperation>().Named("DefaultOperation-Scoped-2")
            //    .ImplementedBy<DefaultOperation>().LifestyleScoped());

            //// two scoped services with the same implementation which resolve to the same value in the same scope
            container.Register(Component
                // type-forwarding : method 1
                //.For<IScopedOperation, IAnotherScopedOperation>()

                // type-forwarding : method 2
                .For<IScopedOperation>().Named("DefaultOperation-Scoped")
                .Forward<IAnotherScopedOperation>()
                .ImplementedBy<DefaultOperation>().LifestyleScoped());

            //// one transient service
            container.Register(Component
                .For<ITransientOperation>().Named("DefaultOperation-Transient")
                .ImplementedBy<DefaultOperation>().LifestyleTransient());


            //// one transient service being its own implementation
            // the easy way!
            container.Register(Component.For<OperationLogger>().LifestyleTransient());

            // the hard way!
            //container.Register(Component
            //    .For<OperationLogger>().UsingFactoryMethod<OperationLogger>(kernel => new OperationLogger(
            //        kernel.Resolve<ITransientOperation>(),
            //        kernel.Resolve<IScopedOperation>(),
            //        kernel.Resolve<IAnotherScopedOperation>(),
            //        kernel.Resolve<ISingletonOperation>()
            //    )).LifestyleTransient());
        }
    }
}
