using System;

namespace Questions.Application.Quizes
{
    public enum QuestionType
    {
        TrueFalse = 0, Options = 1, Text = 2
    }

    public class QuestionTypeConverter
    {
        public static QuestionType ToType(int value)
        {
            return (QuestionType) Enum.ToObject(typeof(QuestionType), value);
        }

        public static int ToValue(QuestionType type)
        {
            return (int) type;
        }
    }
}