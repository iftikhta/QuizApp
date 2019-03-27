using System.Collections.Generic;

namespace Questions.Application.Quizes
{
    public class QuizController
    {
        public QuizController()
        {
            Quizes = new List<Quiz>(); 
        }

        public List<Quiz> Quizes { get; private set; }

        public void Load(List<Quiz> list)
        {
            Quizes = list;
        }

        public void AddQuiz(Quiz quiz)
        {
            Quizes.Add(quiz);
        }

        public void RemoveQuiz(Quiz quiz)
        {
            Quizes.Remove(quiz);
        }
    }
}