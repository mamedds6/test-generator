using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSDTestGenerator.DTO;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // GET api/categories
        [HttpGet]
        public List<Category> Get([FromQuery(Name = "name")] string name = null)
        {
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                List<Category> categories = new List<Category>();
                if (name != null)
                    categories = (List<Category>)quizDbContext.Category.ToList().Where(b => b.Name == name);
                else
                    categories = quizDbContext.Category.ToList();

                return categories;

            }
        }

        // POST api/categories/add
        [HttpPost("add")]
        public ActionResult Post([FromBody] QuestionDto questionDto)
        {
            if (questionDto == null || questionDto.Answers == null || questionDto.Answers.Count <= 0 || string.IsNullOrEmpty(questionDto.Content))
            {
                return BadRequest();
            }

            foreach (AnswerDto answer in questionDto.Answers)
            {
                if (string.IsNullOrEmpty(answer.Content))
                {
                    return BadRequest();
                }
            }

            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                int newQuestionId = quizDbContext.Question.Max(q => q.Id) + 1;
                Question question = questionDto.Question;
                question.Id = newQuestionId;

                ICollection<QuestionAnswer> questionAnswers = question.QuestionAnswer;

                question.QuestionAnswer = new List<QuestionAnswer>();

                quizDbContext.Question.Add(question);



                int newQuestionAnswerId = quizDbContext.QuestionAnswer.Max(q => q.Id) + 1;
                int newAnswerId = quizDbContext.Answer.Max(q => q.Id) + 1;

                foreach (QuestionAnswer questionAnswer in questionAnswers)
                {
                    questionAnswer.Id = newQuestionAnswerId;

                    Answer answer = questionAnswer.Answer;
                    answer.Id = newAnswerId;

                    questionAnswer.Answer = null;
                    questionAnswer.AnswerId = answer.Id;
                    questionAnswer.QuestionId = question.Id;
                    quizDbContext.Answer.Add(answer);

                    quizDbContext.QuestionAnswer.Add(questionAnswer);

                    newQuestionAnswerId++;
                    newAnswerId++;
                }

                quizDbContext.SaveChanges();
            }

            return Ok();
        }

    }
}
