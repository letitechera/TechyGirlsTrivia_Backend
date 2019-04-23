using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.WebAPI.Storage.Tables
{
    public class AnswersTableEntity: TableEntity
    {
        public AnswersTableEntity(string answerId, string questionId)
        {
            this.PartitionKey = answerId;
            this.RowKey = questionId;
        }
        public string AnswerText { get; set; }
        public string AnswerLetter { get; set; }

        public AnswersTableEntity() { }
    }
}
