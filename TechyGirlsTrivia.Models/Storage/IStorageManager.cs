using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Storage.Tables;

namespace TechyGirlsTrivia.Models.Storage
{
    public interface IStorageManager
    {
        Task StoreEntity(ITableEntity entity, string tableName);
        List<ParticipantsTableEntity> SearchNames(string name);
        List<ParticipantsTableEntity> GetAllParticipants(string gameId);
        List<QuestionsTableEntity> GetQuestion();
        Task SaveAnswer(ParticipantsTableEntity participant);
        List<AnswersTableEntity> GetAnswers(int questionId);
        List<CategoryTableEntity> GetCategory(int categoryId);
        Task<string> LoadUserImage(IFormFile file);
        Task UpdateIsAnswered(QuestionsTableEntity question);
        Task ResetAllIsAnswered();
        Task DeleteAllUsers();
    }
}
