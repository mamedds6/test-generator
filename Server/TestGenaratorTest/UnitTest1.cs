using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TSDTestGenerator.DTO;
using TSDTestGenerator.Model;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void DTOQuestionAnswerDTO_QuestionDTO_QuestionDTO()
        {
            QuestionDto inputQuestion = new QuestionDto();
            inputQuestion.Content = "This is a content of first qustion";
            inputQuestion.Answers = new HashSet<AnswerDto>();

            AnswerDto inputeAnswer = new AnswerDto();
            inputeAnswer.Content = "Answer numbre one is correct";
            inputeAnswer.IsCorrect = true;
            inputQuestion.Answers.Append(inputeAnswer);

            Question middle = inputQuestion.Question;
            QuestionDto outputQuestion = new QuestionDto(middle);

            CollectionAssert.AreEqual(inputQuestion.Answers, outputQuestion.Answers);
            Assert.AreSame(inputQuestion.Content, outputQuestion.Content);
        }

        [Test]
        public void DTOQuestionAnswerDTO_AnswerDTO_AnswerDTO()
        {
            AnswerDto inputeAnswer = new AnswerDto();
            inputeAnswer.Content = "Answer numbre one is correct";
            inputeAnswer.IsCorrect = true;

            QuestionAnswer qa = inputeAnswer.QuestionAnswer;
            AnswerDto outputAnswer = new AnswerDto(qa);

            Assert.AreEqual(inputeAnswer.IsCorrect, outputAnswer.IsCorrect);
            Assert.AreSame(inputeAnswer.Content, outputAnswer.Content);
        }
    }
}