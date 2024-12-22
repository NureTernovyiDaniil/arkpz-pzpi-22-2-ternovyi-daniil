using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChefMate_backend.Hubs
{
    public class IoTHub : Hub
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IoTHub(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task JoinGroup(Guid organizationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, organizationId.ToString());
            await Clients.Group(organizationId.ToString()).SendAsync("ReceiveMessage", new { Text = $"{Context.ConnectionId} has joined the group {organizationId}." });
        }

        public async Task SendMessageToGroup(Guid organizationId, string message)
        {
            await Clients.Group(organizationId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
