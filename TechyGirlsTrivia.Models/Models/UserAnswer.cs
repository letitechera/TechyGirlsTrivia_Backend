using System;
using System.Collections.Generic;
using System.Text;

namespace TechyGirlsTrivia.Models
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string ParticipantId { get; set; }
        public int Time { get; set; }
    }
}
