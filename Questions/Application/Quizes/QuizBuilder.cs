using System.Collections.Generic;
using System.Linq;
using Questions.Application.Questions;

namespace Questions.Application.Quizes
{
    public class QuizBuilder
    {
        public static QuizBuilder Create(string name)
        {
            return new QuizBuilder(name);
        }

        private string _name;
        private readonly List<Question> _questions;

        private void AddTrueFalseQuestion(string text, int points, bool answer)
        {
            _questions.Add(new TrueFalseQuestion(answer, text, points));
        }

        private void AddTextQuestion(string text, int points, string answer)
        {
            _questions.Add(new TextQuestion(answer, text, points));
        }

        private void AddOptionsQuestion(string text, int points, Dictionary<string, bool> options)
        {
            var optionsList = options.Keys.ToList();
            List<int> correctList = new List<int>();

            for (int i = 0; i < optionsList.Count; i++)
                if (options[optionsList[0]])
                    correctList.Add(i);

            _questions.Add(new OptionsQuestion(optionsList, correctList, text, points));
        }

        private QuizBuilder(string name)
        {
            _name = name;
            _questions = new List<Question>();
        }

        public string Name => _name;
        public int Count => _questions.Count;

        public QuizBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public QuizBuilder AddQuestion(string text, int points, object value)
        {
            if (value is bool val) AddTrueFalseQuestion(text, points, val);
            if (value is Dictionary<string, bool> dict) AddOptionsQuestion(text, points, dict);
            if (value is string s) AddTextQuestion(text, points, s);
            return this;
        }

        public QuizBuilder RemoveQuestion(int i)
        {
            _questions.RemoveAt(i);
            return this;
        }

        public Quiz Build()
        {
            return new Quiz(_name, _questions);
        }
    }
}