﻿using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;

namespace TechyGirlsTrivia.Models.Storage
{
    public interface IDataAccess
    {
        Task StoreEntity(ITableEntity entity, string tableName);
        IEnumerable<Participant> GetParticipants(string gameId);
        Question GetQuestion();
        bool AlreadyExists(string name);
        Task<string> LoadUserImage(IFormFile file);
        Task SaveAnswerAsync(UserAnswer p);
        Task UpdateIsAnsweredAsync(Question question);
        Task DeleteAllUsers();
        Task ResetAllIsAnswered();
        IEnumerable<Participant> GetWinners(string gameId);
    }
}
