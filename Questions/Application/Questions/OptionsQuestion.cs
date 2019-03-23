using System.Collections.Generic;

namespace Questions.Application.Questions
{
    public class OptionsQuestion: Question
    {
        private readonly List<int> _correctList;

        public OptionsQuestion(List<string> options, List<int> correct, string text, int points) : base(text, points)
        {
            _correctList = correct;
            Options = options;
        }

        public List<string> Options { get; }

        public override int CheckAnswer(object answer)
        {
            if (!(answer is List<int>)) return 0;

            var answerList = (List<int>) answer;

            var correctCount = answerList.FindAll(ans => _correctList.Contains(ans)).Count;

            return GetPoints(_correctList.Count, correctCount);
        }
    }
}