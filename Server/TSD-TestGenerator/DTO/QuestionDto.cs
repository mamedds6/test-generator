using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.DTO
{
    public class QuestionDto
    {
        public string Content { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }


        public QuestionDto(Question question)
        {
            Content = question.Content;
            Answers = question.QuestionAnswer.Select(qe => new AnswerDto(qe)).ToList();
        }
    }
}
