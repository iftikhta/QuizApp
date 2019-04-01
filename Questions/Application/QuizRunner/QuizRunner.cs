using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Questions.Application.Quizes;

namespace Questions.Application.QuizRunner
{
    class QuizRunner
    {
        //ctor not needed?

        public Quiz CurrentQuiz{ get; set; }

        public int CurrentQuestionNum { get; set; } = 0;

        public int Score { get; set; }

        //how many you got right, regardless of points
        public int TotalQuestionsCorrect { get; set; }



        public void NextQuestion()
        {
            if (!IsLastQuestion())
                CurrentQuestionNum += 1;
        }

        public void PreviousQuestion()
        {
            if (!IsFirstQuestion())
                CurrentQuestionNum -= 1;
        }

        public bool IsFirstQuestion()
        {
            if (CurrentQuestionNum == 0)
                return true;
            return false;
        }

        public bool IsLastQuestion()
        {
            if (CurrentQuestionNum == CurrentQuiz.Count - 1)
                return true;
            return false;
        }

        //answers may not need to be stored
        //public List<>
    }
}
