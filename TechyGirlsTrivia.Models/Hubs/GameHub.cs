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
        public async Task BroadcastChartData(List<Question> data) => await Clients.All.SendAsync("broadcastchartdata", data);
    }
}
