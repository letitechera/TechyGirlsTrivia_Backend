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
        public async Task BroadcastQuestion(Question data) => await Clients.All.SendAsync("showquestion", data);
        public async Task BroadcastNewParticipant(bool data) => await Clients.All.SendAsync("newparticipant", data);
        public async Task BroadcastStart(bool data) => await Clients.All.SendAsync("start", data);

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
