using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.WebAPI.Storage.Tables
{
    public class UserAnswerTableEntity: TableEntity
    {
        public UserAnswerTableEntity(string participantId, string questionId)
        {
            this.PartitionKey = participantId;
            this.RowKey = questionId;
        }

        public string AnswerId { get; set; }
        public int Score { get; set; }
        public TimeSpan Time { get; set; }
    }
}
