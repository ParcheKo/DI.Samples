namespace Samples.DI.AspNetCore.ConsoleApp.Lifetime
{
    public class OperationLogger
    {
        private readonly ITransientOperation _transientOperation;
        private readonly IScopedOperation _scopedOperation;
        private readonly IAnotherScopedOperation _anotherScopedOperation;
        private readonly ISingletonOperation _singletonOperation;

        public OperationLogger(
            ITransientOperation transientOperation,
            IScopedOperation scopedOperation,
            IAnotherScopedOperation anotherScopedOperation,
            ISingletonOperation singletonOperation) =>
            (_transientOperation, _scopedOperation, _anotherScopedOperation, _singletonOperation) =
            (transientOperation, scopedOperation, anotherScopedOperation, singletonOperation);

        public void LogOperations(string scope)
        {
            LogOperation(_transientOperation, scope, "Always different");
            LogOperation(_scopedOperation, scope, "Changes only with scope");
            LogOperation(_anotherScopedOperation, scope, "Changes only with scope? YES!");
            LogOperation(_singletonOperation, scope, "Always the same");
        }

        private static void LogOperation<T>(T operation, string scope, string message)
            where T : IOperation =>
            System.Console.WriteLine(
                $"{scope}: {typeof(T).Name,-23} [ {operation.OperationId}...{message,-29} ]");
    }
}
