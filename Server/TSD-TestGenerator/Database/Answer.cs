using System.Collections.Generic;

namespace TSDTestGenerator.Database
{
    public class Answer
    {
        public Answer()
        {
            QuestionAnswer = new HashSet<Database.QuestionAnswer>();
        }

        public int Id { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Database.QuestionAnswer> QuestionAnswer { get; set; }
    }
}
