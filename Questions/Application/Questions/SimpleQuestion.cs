namespace Questions.Application.Questions
{
    public class SimpleQuestion<T> : Question
    {
        private readonly T _answer;

        public SimpleQuestion(T answer, string text, int points) 
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