namespace Questions.Application.Questions
{
    public abstract class SimpleQuestion<T> : Question
    {
        private readonly T _answer;

        protected SimpleQuestion(T answer, string text, int points) 
            : base(text, points)
        {
            _answer = answer;
        }

        public override int CheckAnswer(object answer)
        {
            return _answer.Equals(answer) ? Points : 0;
        }
    }
}