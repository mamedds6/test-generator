using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TSDTestGenerator.DTO;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        // GET api/quiz
        [HttpGet]
        public ActionResult<IEnumerable<QuestionDto>> Get([FromQuery(Name = "number")] int? number,
            [FromQuery(Name = "easyNumber")] int? easyNumber,
            [FromQuery(Name = "mediumNumber")] int? mediumNumber,
            [FromQuery(Name = "hardNumber")] int? hardNumber,
            [FromQuery(Name = "category")] string category)
        {
            if (!number.HasValue)
            {
                number = 10;
            }
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                return new Randomizer().getRandomQuestions(quizDbContext.Question.ToList(), number.Value).Select(q => new QuestionDto(q)).ToList();
            }
        }

        [HttpOptions("question")]
        [EnableCors("MyPolicy")]
        public ActionResult Options()
        {
            return Ok();
        }

        // POST api/quiz/question
        [HttpPost("question")]
        [EnableCors("MyPolicy")]
        public ActionResult Post([FromBody] QuestionDto questionDto)
        {
            if(questionDto == null)
            {
                return BadRequest("question is null");
            }

            if(questionDto.Answers == null)
            {
                return BadRequest("answers are null");
            }

            if(questionDto.Answers.Count <= 0)
            {
                return BadRequest("answers count is 0");
            }

            if(string.IsNullOrEmpty(questionDto.Content))
            {
                return BadRequest("question has no content");
            }

            foreach(AnswerDto answer in questionDto.Answers)
            {
                if (string.IsNullOrEmpty(answer.Content))
                {
                    return BadRequest("answer has no content");
                }
            }

            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                int newQuestionId = quizDbContext.Question.Max(q => q.Id) + 1;
                Question question = questionDto.Question;
                question.Id = newQuestionId;

                ICollection<QuestionAnswer> questionAnswers = question.QuestionAnswer;

                question.QuestionAnswer = new List<QuestionAnswer>();

                question.CategoryId = 0;

                quizDbContext.Question.Add(question);

                

                int newQuestionAnswerId;
                if (quizDbContext.QuestionAnswer.Count() != 0)
                {
                    newQuestionAnswerId = quizDbContext.QuestionAnswer.Max(q => q.Id) + 1;
                }
                else
                {
                    newQuestionAnswerId = 0;
                }

                int newAnswerId;
                if (quizDbContext.Answer.Count() != 0)
                {
                    newAnswerId = quizDbContext.Answer.Max(q => q.Id) + 1;
                }
                else
                {
                    newAnswerId = 0;
                }

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

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }


}
