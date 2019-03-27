using System;
using Questions.Application.Quizes;

namespace Questions.Application.Questions
{
    public class TextQuestion: Question
    {
        private readonly string _answer;

        private string Normalize(string value)
        {
            return value.ToLower().Trim();
        }

        public TextQuestion(string answer, int points) : base(points)
        {
            _answer = Normalize(answer);
        }

        public int Length => _answer.Length;

        public override int CheckAnswer(object answer)
        {
            if (!(answer is string)) return 0;

            string str = Normalize((string) answer);
            int i = 0;

            while (i < Length && str[i] == _answer[i]) i++;

            return GetPoints(Length, i);
        }
    }
}