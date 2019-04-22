using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;

namespace TechyGirlsTrivia.WebAPI.Storage
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
                    Points = p.TotalScore,
                    Time = p.Time,
                    GameId = gameId
                }).ToList();
        }

        public bool AlreadyExists(string name)
        {
            var results = _storageManager.SearchNames(name).ToList();
            return results.Any();
        }

        public Question GetQuestion()
        {
            string questions = System.IO.File.ReadAllText(@"MockData\Question.json");

            var question= JsonConvert.DeserializeObject<List<Question>>(questions).FirstOrDefault(q=>!q.IsAnswered);
            question.Answers = GetAnswers(question.QuestionId);
            question.Category = GetQuestionCategory(question.CategoryId);
            return question;
            //var questions = _storageManager.GetQuestion();
            //if (questions != null)
            //{
            //    var q = questions.ToList().FirstOrDefault();
                
            //    return new Question
            //    {
            //        CategoryId = q.CategoryId,
            //        CorrectAnswerId = q.CorrectAnswerId,
            //        QuestionId = int.Parse(q.RowKey),
            //        QuestionText = q.QuestionText,
            //        Answers = GetAnswers(int.Parse(q.RowKey))
            //    };
            //}
            //return null;
        }
        #region private methods

        private List<Answer> GetAnswers(int questionId)
        {
            string answers = System.IO.File.ReadAllText(@"MockData\Answers.json");

            return JsonConvert.DeserializeObject<List<Answer>>(answers).Where(a=>a.QuestionId == questionId).ToList();
            //var answers = _storageManager.GetAnswers(questionId);
            //return answers == null ? null :
            //     answers.ToList().Select(a => new Answer
            //     {
            //         AnswerId = int.Parse(a.RowKey),
            //         AnswerText = a.AnswerText
            //     }).ToList();
        }

        private Category GetQuestionCategory(int categoryId)
        {
            string categories = System.IO.File.ReadAllText(@"MockData\QuestionCategory.json");

            return JsonConvert.DeserializeObject<List<Category>>(categories).ToList().FirstOrDefault(c =>c.CategoryId == categoryId);
          
        }
        #endregion
    }
}