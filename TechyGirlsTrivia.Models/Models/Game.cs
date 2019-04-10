using System;
using System.Collections.Generic;
using System.Text;

namespace TechyGirlsTrivia.Models.Models
{
    public class Game
    {
        public List<Participant> Gamers { get; set; }
        public List<Question> Questions { get; set; }
    }
}
