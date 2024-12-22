using IoTClientApp.Services;
using Microsoft.Extensions.Configuration;

namespace IoTClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appConfig.json")
                .Build();

            var signalRService = new SignalRService(configuration);

            while (true)
            {
                Console.WriteLine("Enter a key for exit");
                var val = Console.ReadKey();
                Console.WriteLine("Exiting...");
                return;
            }
        }
    }
}
