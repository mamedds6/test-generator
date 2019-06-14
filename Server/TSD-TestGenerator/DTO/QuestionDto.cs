using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.DTO
{
    public class QuestionDto
    {
        public string Content { get; set; }
        public string Difficulty { get; set; }

        public string Category { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }


        public QuestionDto()
        {

        }

        public QuestionDto(Question question)
        {
            Content = question.Content;
            Answers = question.QuestionAnswer.Select(qe => new AnswerDto(qe)).ToList();
            Difficulty = question.Difficulty == 0 ? "Easy" : question.Difficulty == 1 ? "Medium" : "Hard";
            Category = question.Category.Name;
        }

        [JsonIgnore]
        public Question Question => new Question
        {
            Content = Content,
            Id = 0,
            Difficulty = Difficulty.Equals("Easy") ? 0 : Difficulty.Equals("Medium") ? 1 : 2,
            QuestionAnswer = Answers.Select(answer => answer.QuestionAnswer).ToList()
        };
    }
}
