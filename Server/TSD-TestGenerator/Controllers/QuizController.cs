using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Question>> Get()
        {
            return Ok();
            //return new Randomizer().getRandomQuestions(_questionsDbContext.Questions.ToList(), 10);
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

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
