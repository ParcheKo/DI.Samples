using DI.Samples.CastleWindsor.ConsoleApp.Lifetime;
using System.Threading.Tasks;

namespace DI.Samples.CastleWindsor.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const DemoApp selectedChoice = DemoApp.Lifetime;
            switch (selectedChoice)
            {
                case DemoApp.Lifetime:
                    await Default.DemoAsync(args);
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
