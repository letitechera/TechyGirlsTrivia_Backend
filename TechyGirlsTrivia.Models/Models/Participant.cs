using System;
using System.Collections.Generic;
using System.Text;

namespace TechyGirlsTrivia.Models.Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public int Points { get; set; }
        public int Time { get; set; }
    }
}
