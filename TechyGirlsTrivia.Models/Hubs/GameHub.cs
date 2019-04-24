using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models;

namespace TechyGirlsTrivia.Models.Hubs
{
    public class GameHub : Hub
    {
        public async Task BroadcastStart(bool data) => await Clients.All.SendAsync("broadcastStart", data);
        //public async Task BroadcastAnswer(UserAnswer data) => await Clients.All.SendAsync("broadcastAnswer", data);

        public async Task BroadcastAnswer(UserAnswer data)
        {
            await Clients.All.SendAsync("broadcastAnswer", data);
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
