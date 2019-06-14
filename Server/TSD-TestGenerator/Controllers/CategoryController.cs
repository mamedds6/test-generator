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
        public List<Category> Get()
        {
            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                return quizDbContext.Category.ToList();
            }
        }

        // POST api/categories/add
        [HttpPost("add")]
        public ActionResult Post([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest("category is null");
            }
            else if (string.IsNullOrEmpty(category.Name))
            {
                return BadRequest("category name is null");
            }   


            using (QuizDBContext quizDbContext = new QuizDBContext())
            {
                category.Id = quizDbContext.Category.Max(q => q.Id) + 1;
                quizDbContext.Category.Add(category);

                quizDbContext.SaveChanges();
            }

            return Ok();
        }

    }
}
