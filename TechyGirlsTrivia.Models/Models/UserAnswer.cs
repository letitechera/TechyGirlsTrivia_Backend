using System;
using System.Collections.Generic;
using System.Text;

namespace TechyGirlsTrivia.Models.Helpers
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int ParticipantId { get; set; }
        public int Time { get; set; }
    }
}
