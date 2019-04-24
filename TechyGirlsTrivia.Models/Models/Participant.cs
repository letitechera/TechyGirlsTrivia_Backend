using System;
using System.Collections.Generic;
using System.Text;

namespace TechyGirlsTrivia.Models.Models
{
    public class Participant
    {
        public string ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantImg { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string GameId { get; set; }
    }
}
