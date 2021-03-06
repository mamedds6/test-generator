﻿using Newtonsoft.Json;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.DTO
{
    public class AnswerDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerDto()
        {

        }

        public AnswerDto(QuestionAnswer questionAnswer)
        {
            Content = questionAnswer.Answer.Content;
            IsCorrect = questionAnswer.IsCorrect;
        }

        [JsonIgnore]
        public QuestionAnswer QuestionAnswer => new QuestionAnswer
        {
            IsCorrect = IsCorrect,
            Answer = new Answer
            {
                Content = Content,
                Id = 0
            }
        };

    }
}
