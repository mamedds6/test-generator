using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TSDTestGenerator.DTO;
using TSDTestGenerator.Model;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        // GET api/values
        [Route("/")]
        [HttpGet]
        public ActionResult<QuestionDto> Get()
        {
            using (var quizDbContext = new QuizDBContext())
            {
//                quizDbContext.Question.Add(new Question() {Content = "YYY"});
//                quizDbContext.SaveChanges();
//                return Json(new QuestionDto(quizDbContext.Question.First()));
                return Ok(new QuestionDto(new Question(){Id = 10, Content = "XXX"}));
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
