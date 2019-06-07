using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TSD_TestGenerator;
using TSDTestGenerator.Database;

namespace TSDTestGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly QuestionsDbContext _questionsDbContext;

        ValuesController(QuestionsDbContext questionsDbContext)
        {
            _questionsDbContext = questionsDbContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Question>> Get()
        {
            return _questionsDbContext.Questions;
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
