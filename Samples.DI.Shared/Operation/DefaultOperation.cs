using System;

namespace Samples.DI.Shared.Operation
{
    public class DefaultOperation :
        ITransientOperation,
        IScopedOperation,
        IAnotherScopedOperation,
        ISingletonOperation
    {
        public string OperationId { get; } = Guid.NewGuid().ToString()[^4..];

        public DefaultOperation()
        {
            
        }

        public override string ToString()
        {
            return OperationId;
        }
    }
}
