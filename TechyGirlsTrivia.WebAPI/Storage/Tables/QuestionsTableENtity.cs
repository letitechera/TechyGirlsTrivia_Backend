using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.WebAPI.Storage.Tables
{
    public class QuestionsTableEntity: TableEntity
    {
        public QuestionsTableEntity(string questionId, string questionNumber)
        {
            this.PartitionKey = questionId;
            this.RowKey = questionNumber;
        }
        public QuestionsTableEntity()
        {
        }

        public string QuestionText { get; set; }
        public int CorrectAnswerId { get; set; }
        public bool IsAnswered { get; set; }
        public int CategoryId { get; set; }
    }
}
