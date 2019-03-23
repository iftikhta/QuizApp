using System.Collections.Generic;

namespace Questions.Application.Quizes
{
    public class QuizController
    {
        private readonly QuizLoader _loader;

        public QuizController(QuizLoader loader)
        {
            _loader = loader;
            Quizes = null;  // TODO: Think about loading quizes / Factory / etc.
        }

        public bool IsLoaded => Quizes != null;
        public List<Quiz> Quizes { get; private set; }

        public void Load()
        {
            Quizes = _loader.Load();
        }

        public void AddQuiz(Quiz quiz)
        {
            Quizes.Add(quiz);
            _loader.Save(Quizes); // Atomic Updates
        }

        public void RemoveQuiz(Quiz quiz)
        {
            Quizes.Remove(quiz);
            _loader.Save(Quizes);
        }
    }
}