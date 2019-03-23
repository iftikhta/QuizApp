namespace Questions.Application.Questions
{
    public class TrueFalseQuestion: SimpleQuestion<bool>
    {
        public TrueFalseQuestion(bool answer, string text, int points)
            : base(answer, text, points)
        {
        }
    }
}