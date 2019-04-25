using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;
using TechyGirlsTrivia.Models.Storage;
using TechyGirlsTrivia.Models.Storage.Tables;

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

        public async Task RegisterUser(Participant p)
        {
            p.ParticipantId = Guid.NewGuid().ToString();
            var pEntity = new ParticipantsTableEntity(p);

            // dummy add & send list
            var returnList = _dataAccess.GetParticipants(p.GameId) as List<Participant>;
            returnList.Add(p);
            await Clients.All.SendAsync("registerUser", returnList);

            // save
            await _dataAccess.StoreEntity(pEntity, "Participants");
        }

        public async Task SetAnswer(UserAnswer data)
        {
            await Clients.All.SendAsync("setAnswer", data);
            await _dataAccess.SaveAnswerAsync(data);
        }

        public async Task FinalResults(string gameId)
        {
            var allUsers = _dataAccess.GetParticipants(gameId) as List<Participant>;
            var winners = allUsers.OrderByDescending(g => g.Score).Take(3);
            await Clients.All.SendAsync("finalResults", winners);
        }

    }
}
