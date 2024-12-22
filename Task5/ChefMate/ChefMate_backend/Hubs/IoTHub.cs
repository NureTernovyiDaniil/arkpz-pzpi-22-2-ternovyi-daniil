using ChefMate_backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
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

        [Authorize(Roles = "IoTDevice")]
        public async Task JoinGroup(Guid workZoneId)
        {
            var organizationClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == "OrganizationId");

            if (organizationClaim == null)
            {
                throw new InvalidOperationException("OrganizationId is missing in the JWT token.");
            }

            var organizationId = new Guid(organizationClaim.Value);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var workZoneRepository = scope.ServiceProvider.GetService<IWorkZoneRepository>();
                var isExist = await workZoneRepository.IsExistInOrganization(workZoneId, organizationId);

                if(!isExist)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, workZoneId.ToString());
                    await Clients.Group(workZoneId.ToString()).SendAsync("ReceiveMessage", new { Text = $"Incorrect work zone identifier." });
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, workZoneId.ToString());

                    return;
                }
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, workZoneId.ToString());
            await Clients.Group(workZoneId.ToString()).SendAsync("ReceiveMessage", new { Text = $"{Context.ConnectionId} has joined the group {workZoneId}." });
        }

        public async Task SendMessageToGroup(Guid workZoneId, object model)
        {
            var serializedValue = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            await Clients.Group(workZoneId.ToString()).SendAsync("ReceiveModel", serializedValue);
        }
    }
}
