using System;
using System.Collections.Generic;
using System.Text;

namespace TechyGirlsTrivia.Models.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
        public int CorrectAnswerId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsAnswered { get; set; }
    }
}
