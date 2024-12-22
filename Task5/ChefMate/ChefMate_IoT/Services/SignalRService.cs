using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace IoTClientApp.Services
{
    public class SignalRService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly HubConnection _hubConnection;
        private string _jwtToken;

        public SignalRService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();

            var connectionString = _configuration["SignalR:HubBaseUrl"];
            var clientGuid = new Guid(_configuration["SignalR:IoTClientGuid"]);

            RequestJwtToken().Wait();

            _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{connectionString}/IoT", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(_jwtToken);
            })
            .Build();

            Connect(clientGuid).Wait();

            _hubConnection.On<string>("ReceiveMessage", message =>
            {
                Console.WriteLine("Received from SignalR: \n" + message);
            });

            _hubConnection.On<string>("ReceiveModel", Console.WriteLine);
        }

        private async Task RequestJwtToken()
        {
            var authApiUrl = _configuration["API:ApiBaseUrl"];
            var email = _configuration["Auth:Email"];
            var password = _configuration["Auth:Password"];

            var loginPayload = new
            {
                Email = email,
                Password = password
            };

            var content = new StringContent(JsonSerializer.Serialize(loginPayload), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync($"{authApiUrl}/Auth/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);
                    _jwtToken = tokenResponse.token;

                    Console.WriteLine("JWT token obtained successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to obtain JWT token: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during token request: {ex.Message}");
            }
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

        private class TokenResponse
        {
            public string token { get; set; }
        }
    }
}
