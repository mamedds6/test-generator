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
    public class QuizController : ControllerBase
    {
        // GET api/quiz
        [HttpGet]
        public ActionResult<IEnumerable<QuestionDto>> Get([FromQuery(Name = "number")] int? number)
        {
            if (!number.HasValue)
            {
                number = 10;
            }
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                List<Question> questions = quizDbContext.Question.ToList();
                List<QuestionAnswer> questionAnswers = quizDbContext.QuestionAnswer.ToList();
                List<Answer> answers = quizDbContext.Answer.ToList();
                foreach (QuestionAnswer questionAnswer in questionAnswers)
                {
                    questionAnswer.Answer = answers.Find(answer => answer.Id == questionAnswer.AnswerId);
                    questions.Find(question => question.Id == questionAnswer.QuestionId).QuestionAnswer.Add(questionAnswer);
                }
                return new Randomizer().getRandomQuestions(quizDbContext.Question.ToList(), number.Value).Select(q => new QuestionDto(q)).ToList();
            }
        }

        // POST api/quiz/question
        [HttpPost("question")]
        public void Post([FromBody] Question question)
        {

        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }


}
