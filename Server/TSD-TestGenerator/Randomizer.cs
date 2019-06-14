using System.Collections.Generic;
using System;
using TSDTestGenerator.Model;


namespace TSDTestGenerator.Controllers
{
    public class Randomizer
    {
        public Randomizer()
        {

        }

        public List<Question> getRandomQuestions(List<Question> allQuestions, int numberOfQuestions)
        {
            Random rand = new Random();
            List<Question> questions = new List<Question>();
            if (allQuestions.Count <= numberOfQuestions)
            {
                return allQuestions;
            }

            while (questions.Count < numberOfQuestions)
            {
                Question question = allQuestions[rand.Next(0, allQuestions.Count)];
                if (!questions.Contains(question))
                {
                    questions.Add(question);
                }
            }

            return questions;
        }
    }
}
