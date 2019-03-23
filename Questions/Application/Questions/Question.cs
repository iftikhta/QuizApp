using System;

namespace Questions.Application.Questions
{
    public abstract class Question
    {
        protected int GetPoints(int total, int correct)
        {
            var score = Points * (correct / total);
            return (int)Math.Round((double)score, MidpointRounding.AwayFromZero);
        }

        protected Question(string text, int points)
        {
            Text = text;
            Points = points;
        }

        public string Text { get; }
        public int Points { get; }

        public abstract int CheckAnswer(object answer);
    }
}