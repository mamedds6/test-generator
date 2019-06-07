using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSDTestGenerator.Database
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public ICollection<Answer> Answers => QuestionAnswers.Select(qe => qe.Answer).ToList();

        public ICollection<Answer> CorrectAnswers => QuestionAnswers.Where(qe => qe.IsCorrect).Select(qe => qe.Answer).ToList();
    }
}
