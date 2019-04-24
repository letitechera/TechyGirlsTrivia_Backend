using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Storage;

namespace TechyGirlsTrivia.Models.Hubs
{
    public class GameHub : Hub
    {
        private readonly IDataAccess _dataAccess;

        public GameHub(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;

        }

        public async Task StartGame(bool data) => await Clients.All.SendAsync("startGame", data);

        public async Task SetAnswer(UserAnswer data)
        {
            await Clients.All.SendAsync("setAnswer", data);
            await _dataAccess.SaveAnswerAsync(data);
        }

        
    }
}
