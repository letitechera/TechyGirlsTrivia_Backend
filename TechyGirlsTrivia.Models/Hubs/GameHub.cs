using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;

namespace TechyGirlsTrivia.Models.Hubs
{
    public class GameHub: Hub
    {
        public async Task BroadcastStart(bool data) => await Clients.All.SendAsync("broadcastStart", data);

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
