using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.WebAPI.Storage.Tables;

namespace TechyGirlsTrivia.WebAPI.Storage
{
    public interface IStorageManager
    {
        Task StoreEntity(ITableEntity entity, string tableName);
        List<ParticipantsTableEntity> SearchNames(string name);
        List<ParticipantsTableEntity> GetAllParticipants(string gameId);
        List<QuestionsTableEntity> GetQuestion(int questionId);
        List<AnswersTableEntity> GetAnswers(int questionId);
        List<CategoryTableEntity> GetCategory(int categoryId);
        Task<string> LoadUserImage(IFormFile file);
    }
}
