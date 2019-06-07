using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public QuestionDto(Question question)
        {
            Id = question.Id;
            Content = question.Content;
        }
    }
}
