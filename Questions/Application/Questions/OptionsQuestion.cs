using System.Collections.Generic;
using System.Linq;
using Questions.Application.Quizes;

namespace Questions.Application.Questions
{
    public class OptionsQuestion: Question
    {
        private readonly Dictionary<string, bool> _options;

        public OptionsQuestion(Dictionary<string, bool> options, int points) : base(points)
        {
            _options = options;
        }

        public List<string> Options => _options.Keys.ToList();

        public override int CheckAnswer(object answer)
        {
            if (!(answer is List<string>)) return 0;

            var answerList = (List<string>) answer;

            var correctCount = answerList.FindAll(ans => _options[ans]).Count;

            return GetPoints(_options.Count, correctCount);
        }
    }
}