using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.Models.Storage.Tables
{
    public class QuestionsTableEntity: TableEntity
    {
        public QuestionsTableEntity(string questionId, string categoryId)
        {
            this.PartitionKey = questionId;
            this.RowKey = categoryId;
        }

        public QuestionsTableEntity() { }

        public string QuestionText { get; set; }
        public int CorrectAnswerId { get; set; }
    }
}
