using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;
using TechyGirlsTrivia.Models.Storage.Tables;

namespace TechyGirlsTrivia.Models.Storage
{
    public class DataAccess : IDataAccess
    {
        private readonly IStorageManager _storageManager;

        public DataAccess(IStorageManager storageManager)
        {
            _storageManager = storageManager;
        }

        public async Task StoreEntity(ITableEntity entity, string tableName)
        {
            await _storageManager.StoreEntity(entity, tableName);
        }

        public IEnumerable<Participant> GetParticipants(string gameId)
        {
            return _storageManager.GetAllParticipants(gameId)
                .Select(p => new Participant
                {
                    ParticipantId = p.RowKey,
                    ParticipantName = p.ParticipantName,
                    ParticipantImg = p.ParticipantImg,
                    Score = p.TotalScore,
                    Time = p.Time,
                    GameId = gameId
                }).ToList();
        }

        public Question GetQuestion()
        {
            var question = _storageManager.GetQuestion()
                .Select(q => new Question
                {
                    CategoryId = int.Parse(q.RowKey),
                    QuestionId = int.Parse(q.PartitionKey),
                    CorrectAnswerId = q.CorrectAnswerId,
                    QuestionText = q.QuestionText,
                    Answers = GetAnswers(int.Parse(q.PartitionKey)).ToList(),
                    Category = GetCategory(int.Parse(q.RowKey)),
                    IsAnswered = true
                }).FirstOrDefault();

            return question;
        }

        public async Task SaveAnswerAsync(UserAnswer p)
        {
            var userScore = new ParticipantsTableEntity
            {
                PartitionKey = p.GameId,
                RowKey = p.ParticipantId,
                TotalScore = p.Score,
                Time = p.Time,
            };

            await _storageManager.SaveAnswer(userScore);
        }

        public async Task UpdateIsAnsweredAsync(Question question)
        {
            var questionEntity = new QuestionsTableEntity
            {
                CorrectAnswerId = question.CorrectAnswerId,
                PartitionKey = question.QuestionId.ToString(),
                QuestionText = question.QuestionText,
                RowKey = question.CategoryId.ToString(),
                IsAnswered = true
            };

            await _storageManager.UpdateIsAnswered(questionEntity);

        }

        public bool AlreadyExists(string name)
        {
            var results = _storageManager.SearchNames(name).ToList();
            return results.Any();
        }

        public async Task<string> LoadUserImage(IFormFile file)
        {
            return await _storageManager.LoadUserImage(file);
        }

        public IEnumerable<Participant> GetWinners(string gameId)
        {
            return _storageManager.GetAllParticipants(gameId)
                .Select(p => new Participant
                {
                    ParticipantId = p.RowKey,
                    ParticipantName = p.ParticipantName,
                    ParticipantImg = p.ParticipantImg,
                    Score = p.TotalScore,
                    Time = p.Time,
                    GameId = gameId
                }).ToList().OrderByDescending(p=>p.Score).OrderByDescending(p=>p.Time).Take(3);
        }

        public async Task DeleteAllUsers()
        {
            await _storageManager.DeleteAllUsers();
        }
        public async Task ResetAllIsAnswered()
        {
            await _storageManager.ResetAllIsAnswered();
        }

        private IEnumerable<Answer> GetAnswers(int questionId)
        {
            return _storageManager.GetAnswers(questionId).Select(a => new Answer
            {
                AnswerId = int.Parse(a.PartitionKey),
                AnswerText = a.AnswerText,
                AnswerLetter = a.AnswerLetter
            });
        }

        private Category GetCategory(int categoryId)
        {
            return _storageManager.GetCategory(categoryId).Select(c => new Category
            {
                CategoryId = int.Parse(c.PartitionKey),
                CategoryName = c.RowKey,
                CategoryLogo = c.CategoryLogo
            }).FirstOrDefault();
        }

    }
}
