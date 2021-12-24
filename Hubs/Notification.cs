using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ProjectTracker_API.Hubs
{
    public class Notification : Hub
    {
        public async Task AddGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }
    }
}
