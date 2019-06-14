using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TSDTestGenerator.DTO;
using TSDTestGenerator.Model;
using TSDTestGenerator.Controllers;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        // GET api/quiz
        [HttpGet]
        [EnableCors("MyPolicy")]
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
            List<QuestionDto> response = new List<QuestionDto>();
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                if (easyNumber.HasValue)
                {
                    if (!string.IsNullOrEmpty(category) && quizDbContext.Category.Any(c => c.Name.Equals(category)))
                    {
                        int categoryId = quizDbContext.Category.First(c => c.Name.Equals(category)).Id;
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.Difficulty == 0 && q.CategoryId == categoryId).ToList(), easyNumber.Value).Select(q => new QuestionDto(q)).ToList());
                    }
                    else
                    {
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.Difficulty == 0).ToList(), easyNumber.Value).Select(q => new QuestionDto(q)).ToList());
                    }

                }

                if (mediumNumber.HasValue)
                {
                    if (!string.IsNullOrEmpty(category) && quizDbContext.Category.Any(c => c.Name.Equals(category)))
                    {
                        int categoryId = quizDbContext.Category.First(c => c.Name.Equals(category)).Id;
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.Difficulty == 1 && q.CategoryId == categoryId).ToList(), mediumNumber.Value).Select(q => new QuestionDto(q)).ToList());
                    }
                    else
                    {
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.Difficulty == 1).ToList(), mediumNumber.Value).Select(q => new QuestionDto(q)).ToList());
                    }
                }

                if (hardNumber.HasValue)
                {
                    if (!string.IsNullOrEmpty(category) && quizDbContext.Category.Any(c => c.Name.Equals(category)))
                    {
                        int categoryId = quizDbContext.Category.First(c => c.Name.Equals(category)).Id;
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.Difficulty == 2 && q.CategoryId == categoryId).ToList(), hardNumber.Value).Select(q => new QuestionDto(q)).ToList());
                    }
                    else
                    {
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.Difficulty == 2).ToList(), hardNumber.Value).Select(q => new QuestionDto(q)).ToList());
                    }

                }

                if (!easyNumber.HasValue && !mediumNumber.HasValue && !hardNumber.HasValue)
                {
                    if (!string.IsNullOrEmpty(category) && quizDbContext.Category.Any(c => c.Name.Equals(category)))
                    {
                        int categoryId = quizDbContext.Category.First(c => c.Name.Equals(category)).Id;
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.Where(q => q.CategoryId == categoryId).ToList(), number.Value).Select(q => new QuestionDto(q)).ToList());
                    }
                    else
                    {
                        response.AddRange(new Randomizer().getRandomQuestions(quizDbContext.Question.ToList(), number.Value).Select(q => new QuestionDto(q)).ToList());
                    }

                }
            }

            return Ok(response);
        }

        [HttpGet("all")]
        [EnableCors("MyPolicy")]
        public ActionResult<IEnumerable<QuestionDto>> GetAll()
        {
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                return quizDbContext.Question.Select(q => new QuestionDto(q)).ToList();
            }
        }

        // GET api/quiz
        [HttpGet("createQuestions")]
        public ActionResult GenerateQuestions()
        {
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                int newQuestionId = quizDbContext.Question.Count() != 0 ? quizDbContext.Question.Max(q => q.Id) + 1 : 0;
                int newAnswerId = quizDbContext.Answer.Count() != 0 ? quizDbContext.Answer.Max(q => q.Id) + 1 : 0;
                int newQuestionAnswerId = quizDbContext.QuestionAnswer.Count() != 0 ? quizDbContext.QuestionAnswer.Max(q => q.Id) + 1 : 0;
                for (int i = 0; i < 20; i++)
                {
                    Question question = new Question()
                    {
                        Content = $"Pytanie nr {i}",
                        Id = newQuestionId + i,
                        CategoryId = 0,
                        Difficulty = i % 3
                    };

                    quizDbContext.Question.Add(question);

                    for (int j = 0; j < 3; j++)
                    {
                        Answer answer = new Answer()
                        {
                            Id = newAnswerId + i * 3 +j,
                            Content = $"Odpowiedź {j}"
                        };

                        QuestionAnswer questionAnswer = new QuestionAnswer()
                        {
                            Id = newQuestionAnswerId + i * 3 + j,
                            IsCorrect = j == 0,
                            QuestionId = question.Id,
                            AnswerId = answer.Id
                        };

                        quizDbContext.Answer.Add(answer);
                        quizDbContext.QuestionAnswer.Add(questionAnswer);
                    }
                }

                quizDbContext.SaveChanges();
            }

            return Ok();
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
            if (questionDto == null)
            {
                return BadRequest("question is null");
            }

            if (questionDto.Answers == null)
            {
                return BadRequest("answers are null");
            }

            if (questionDto.Answers.Count <= 0)
            {
                return BadRequest("answers count is 0");
            }

            if (string.IsNullOrEmpty(questionDto.Content))
            {
                return BadRequest("question has no content");
            }

            foreach (AnswerDto answer in questionDto.Answers)
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
