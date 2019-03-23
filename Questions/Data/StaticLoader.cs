using System.Collections.Generic;
using Questions.Application.Quizes;

namespace Questions.Data
{
    public class StaticLoader: QuizLoader
    {
        private List<Quiz> _quizes;

        private void Initialize()
        {
            _quizes.Add(QuizBuilder.create("Quiz 1").AddQuestion<bool>("True", 5, true).AddQuestion<bool>("False", 5, false).Build());
            _quizes.Add(QuizBuilder.create("Quiz 2").AddTextQuestion("1+2", 1, "3").AddTextQuestion("ABC..", 2, "DE").Build());
            _quizes.Add(QuizBuilder.create("Quiz 3").AddOptionsQuestion("1,3", 4, new Dictionary<string, bool>()
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