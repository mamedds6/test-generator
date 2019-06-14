using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace TSDTestGenerator.Database
{
    public class QuizDBContext : DbContext
    {
        public QuizDBContext()
        {

        }

//        public QuizDBContext(DbContextOptions<QuizDBContext> options)
//            : base(options)
//        {
//        }

        public virtual Microsoft.EntityFrameworkCore.DbSet<Answer> Answer { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<Question> Question { get; set; }
        public virtual Microsoft.EntityFrameworkCore.DbSet<QuestionAnswer> QuestionAnswer { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDBv3;Trusted_Connection=True;");
//            }
//        }
    }
}
