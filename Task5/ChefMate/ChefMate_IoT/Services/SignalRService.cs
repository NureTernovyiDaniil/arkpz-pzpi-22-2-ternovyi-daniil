using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace IoTClientApp.Services
{
    public class SignalRService
    {
        private readonly IConfiguration _configuration;
        private readonly HubConnection _hubConnection;

        public SignalRService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["SignalR:HubBaseUrl"];
            var clientGuid = new Guid(_configuration["SignalR:IoTClientGuid"]);

            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{connectionString}/IoT")
                .Build();

            Connect(clientGuid).Wait();

            _hubConnection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine("Received from SignalR: \n" + $"{JsonConvert.SerializeObject(message)}");
            });

            _hubConnection.On<string>("ReceiveModel", model =>
            {
                //var json = JsonConvert.SerializeObject(model, Formatting.Indented);
                Console.WriteLine(model);
            });
        }

        private async Task Connect(Guid clientGuid)
        {
            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine($"IoT client connected with GUID: {clientGuid}");

                await _hubConnection.InvokeAsync("JoinGroup", clientGuid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to SignalR: " + ex.Message);
            }
        }
    }
}
