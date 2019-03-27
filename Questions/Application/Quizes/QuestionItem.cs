using System.Collections.Generic;
using Questions.Application.Questions;

namespace Questions.Application.Quizes
{
    public class QuestionItem
    {
        public string Text { get; set; }
        public int Points { get; set; }
        public QuestionType Type { get; set; }
        public object Answer { get; set; }

        public Question ToQuestion()
        {
            switch (Type)
            {
                case QuestionType.Options:
                    return new OptionsQuestion((Dictionary<string, bool>) Answer, Points);
                case QuestionType.TrueFalse:
                    return new SimpleQuestion<bool>((bool)Answer, Points);
                default:
                    return new TextQuestion((string)Answer, Points);
            }
        }
    }
}