using System;

namespace DI.Samples.CastleWindsor.ConsoleApp.Lifetime
{
    public class DefaultOperation :
        ITransientOperation,
        IScopedOperation,
        ISingletonOperation
    {
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];
    }
}
