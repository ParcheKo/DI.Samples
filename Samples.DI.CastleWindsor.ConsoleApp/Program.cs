using System.Threading.Tasks;
using Samples.DI.CastleWindsor.ConsoleApp.Lifetime;

namespace Samples.DI.CastleWindsor.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const DemoApp selectedChoice = DemoApp.Lifetime;
            switch (selectedChoice)
            {
                case DemoApp.Lifetime:
                    await Default.LifetimeDemoAsync(args);
                    break;
            }
        }
    }

    public enum DemoApp
    {
        Lifetime,
        Features
    }
}
