using Questions.Application.Quizes;

namespace Questions.Application.Questions
{
    public class SimpleQuestion<T> : Question
    {
        private readonly T _answer;

        public SimpleQuestion(T answer, int points) 
            : base(points)
        {
            _answer = answer;
        }

        public override int CheckAnswer(object answer)
        {
            return _answer.Equals(answer) ? _points : 0;
        }
    }
}