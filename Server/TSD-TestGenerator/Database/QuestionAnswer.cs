namespace TSDTestGenerator.Database
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
    }
}
