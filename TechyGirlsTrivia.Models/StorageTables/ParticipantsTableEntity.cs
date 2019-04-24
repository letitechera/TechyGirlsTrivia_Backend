using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;

namespace TechyGirlsTrivia.Models.Storage.Tables
{
    public class ParticipantsTableEntity : TableEntity
    {
        public string ParticipantName { get; set; }
        public string ParticipantImg { get; set; }
        public int TotalScore { get; set; }
        public int Time { get; set; }

        public ParticipantsTableEntity(string gameId, string participantId)
        {
            this.PartitionKey = gameId;
            this.RowKey = participantId;
        }

        public ParticipantsTableEntity() { }

        public ParticipantsTableEntity(Participant p) {
            PartitionKey = p.GameId;
            RowKey = p.ParticipantId;
            ParticipantName = p.ParticipantName;
            ParticipantImg = p.ParticipantImg;
            TotalScore = p.Points;
            Time = p.Time;
        }


    }
}
