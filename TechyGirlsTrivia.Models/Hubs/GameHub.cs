using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.Models.Hubs
{
    public class GameHub : Hub
    {
        private readonly IConfiguration Configuration;

        public GameHub(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public async Task StartGame(bool data) => await Clients.All.SendAsync("startGame", data);

        public async Task SetAnswer(UserAnswer data)
        {
            await Clients.All.SendAsync("setAnswer", data);


        }

        
    }
}
