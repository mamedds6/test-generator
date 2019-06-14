using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TSDTestGenerator.Model
{
    public partial class QuizDBContext : DbContext
    {
        public QuizDBContext()
        {
            ReassignData();
        }

        public QuizDBContext(DbContextOptions<QuizDBContext> options)
            : base(options)
        {
        }

        public void ReassignData()
        {
            foreach (QuestionAnswer questionAnswer in QuestionAnswer)
            {
                questionAnswer.Answer = Answer.ToList().Find(answer => answer.Id == questionAnswer.AnswerId);
                Question question = Question.ToList().Find(q => q.Id == questionAnswer.QuestionId);
                question.QuestionAnswer.Add(questionAnswer);
                questionAnswer.Question = question;
            }

            foreach (Question question in Question)
            {
                Category category = Category.ToList().Find(c => c.Id == question.CategoryId);
                category.Question.Add(question);
                question.Category = category;
            }
        }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QuizDB;Trusted_Connection=True;");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Question_Category");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.QuestionAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QuestionAnswer_Answer");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_QuestionAnswer_Question");
            });
        }
    }
}
