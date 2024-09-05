using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSys
{
    internal class Question
    {
        public string QuestionText { get; set; }
        public List<string> AnswerOptions { get; set; }
        public string CorrectAnswer { get; set; }
        public int Grade { get; set; }

        public Question(string questionText, List<string> answerOptions, string correctAnswer, int grade)
        {
            QuestionText = questionText;
            AnswerOptions = answerOptions;
            CorrectAnswer = correctAnswer;
            Grade = grade;
        }
    }
}
