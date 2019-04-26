using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;
using TechyGirlsTrivia.Models.Storage;
using TechyGirlsTrivia.Models.Storage.Tables;
using TechyGirlsTrivia.Models.Helpers;

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

        public async Task EndGame(bool data) => await Clients.All.SendAsync("endGame", data);

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

        public async Task GetAllUsers(string gameId)
        {
            var returnList = _dataAccess.GetParticipants(gameId) as List<Participant>;
            await Clients.All.SendAsync("getAllUsers", returnList);
        }

        public async Task GetQuestion()
        {
            var timerManager = new TimerManager(() => Clients.All.SendAsync("getQuestion", SendQuestion()));
        }

        private Question SendQuestion()
        {
            var question = _dataAccess.GetQuestion();
            if (question != null)
            {
                _dataAccess.UpdateIsAnsweredAsync(question);
            }
            return question;
        }

        public async Task SetAnswer(UserAnswer data)
        {
            await Clients.All.SendAsync("setAnswer", data);
            await _dataAccess.SaveAnswerAsync(data);
        }

        public async Task FinalResults(string gameId)
        {
            var allUsers = _dataAccess.GetParticipants(gameId) as List<Participant>;
            var winners = allUsers.OrderByDescending(g => g.Score).OrderByDescending(g => g.Time).Take(3);
            await Clients.All.SendAsync("finalResults", winners);
        }

    }
}
