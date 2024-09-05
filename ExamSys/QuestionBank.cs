using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSys
{
    internal class QuestionBank
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }
}
