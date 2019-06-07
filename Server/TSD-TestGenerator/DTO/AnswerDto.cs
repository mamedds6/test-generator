using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.DTO
{
    public class AnswerDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerDto(QuestionAnswer questionAnswer)
        {
            Content = questionAnswer.Answer.Content;
            IsCorrect = questionAnswer.IsCorrect;
        }
    }
}
