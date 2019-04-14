using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.WebAPI.Storage.Tables
{
    public class ParticipantsTableEntity : TableEntity
    {
        public ParticipantsTableEntity(string gameId, string participantId)
        {
            this.PartitionKey = gameId;
            this.RowKey = participantId;
        }

        public string ParticipantName { get; set; }
        public string ParticipantImg { get; set; }
        public int TotalScore { get; set; }
        public int Time { get; set; }
    }
}
