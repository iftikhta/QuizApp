using System;
using Questions.Application.Quizes;

namespace Questions.Application.Questions
{
    public abstract class Question
    {
        protected readonly int _points;

        protected int GetPoints(int total, int correct)
        {
            var score = _points * (correct / total);
            return (int)Math.Round((double)score, MidpointRounding.AwayFromZero);
        }

        protected Question(int points)
        {
            _points = points;
        }

        public abstract int CheckAnswer(object answer);
    }
}