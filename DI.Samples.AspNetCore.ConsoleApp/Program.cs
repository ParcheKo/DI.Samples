using System.Threading.Tasks;

namespace Samples.DI.AspNetCore.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const DemoApp selectedChoice = DemoApp.Lifetime;
            switch (selectedChoice)
            {
                case DemoApp.Lifetime:
                    await Lifetime.Default.DemoAsync(args);
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
