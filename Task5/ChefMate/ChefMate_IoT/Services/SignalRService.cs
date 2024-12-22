using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

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
                Console.WriteLine("Received from SignalR: " + message);
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

        public async Task SendMessageToIoTClient(Guid clientGuid, string message)
        {
            try
            {
                await _hubConnection.InvokeAsync("SendMessageToClient", clientGuid, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending message: " + ex.Message);
            }
        }
    }
}
