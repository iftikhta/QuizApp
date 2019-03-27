using System.Collections.Generic;
using Questions.Application.Quizes;

namespace Questions.Data
{
    public class StaticLoader: QuizLoader
    {
        private List<Quiz> _quizes;

        private void Initialize()
        {
            _quizes.Add(QuizBuilder.Create("Quiz 1").AddQuestion("True", 5, true).AddQuestion("False", 5, false).Build());
            _quizes.Add(QuizBuilder.Create("Quiz 2").AddQuestion("1+2", 1, "3").AddQuestion("ABC..", 2, "DE").Build());
            _quizes.Add(QuizBuilder.Create("Quiz 3").AddQuestion("1,3", 4, new Dictionary<string, bool>()
            {
                {"1", true }, {"2", false }, {"3", true }, {"4", false }
            }).Build());
        }

        public StaticLoader()
        {
            _quizes = new List<Quiz>();
            Initialize();
        }

        public List<Quiz> Load()
        {
            return _quizes;
        }

        public void Save(List<Quiz> list)
        {
            _quizes = list;
        }
    }
}