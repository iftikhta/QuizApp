using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Questions.Application.Questions;
using Questions.Application.Quizes;

namespace Questions.Application.QuizRunner
{
    class Answer
    {
        public QuestionType QuestionType { get; set; }
        public object GivenAnswer { get; set; }
        public float Points { get; set; }

    }
}
