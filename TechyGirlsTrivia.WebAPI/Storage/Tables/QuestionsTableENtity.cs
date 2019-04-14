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

        public string QuestionText { get; set; }
        public string CorrectAnswerId { get; set; }
        public string CategoryId { get; set; }
    }
}
