using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TSDTestGenerator.Database
{
    public class QuestionsDbContext : DbContext
    {
        public QuestionsDbContext(DbContextOptions<QuestionsDbContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
    }

}
