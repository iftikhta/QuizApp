using System.Collections.Generic;

namespace Questions.Application.Quizes
{
    public interface QuizLoader
    {
        List<Quiz> Load();
        void Save(List<Quiz> quizes);
    }
}