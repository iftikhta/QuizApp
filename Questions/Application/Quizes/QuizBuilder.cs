using Questions.Application.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Protection.PlayReady;

namespace Questions.Application.Quizes
{
    public enum CreationType { TrueFalse, Options, Text }

    public class QuestionData
    {
        public string Text { get; set; }
        public int Points { get; set; }
        public object Answer { get; set; }
        public CreationType Type { get; set; }
    }

    public class QuizBuilder
    {
        public static QuizBuilder Create(string name = "")
        {
            return new QuizBuilder(name);
        }

        private string _name;
        private readonly List<QuestionData> _questions;

        // TODO: Think about moving away
        private static Question GetOptionsQuestion(Dictionary<string, bool> options, string text, int points)
        {
            var optionsList = options.Keys.ToList();
            List<int> correctList = new List<int>();

            for (int i = 0; i < optionsList.Count; i++)
                if (options[optionsList[0]])
                    correctList.Add(i);

            return new OptionsQuestion(optionsList, correctList, text, points);
        }

        private static Question MapQuestionData(QuestionData data)
        {
            switch (data.Type)
            {
                case CreationType.Options:
                    return GetOptionsQuestion((Dictionary<string, bool>)data.Answer, data.Text, data.Points);
                case CreationType.Text:
                    return new TextQuestion((string)data.Answer, data.Text, data.Points);
                default:
                    return new TrueFalseQuestion((bool) data.Answer, data.Text, data.Points);
            }
        }

        private static CreationType GetCreationType(object value)
        {
            if (value is string) return CreationType.Text;
            if (value is bool) return CreationType.TrueFalse;
            return CreationType.Options;
        }

        private QuizBuilder(string name)
        {
            _name = name;
            _questions = new List<QuestionData>();
        }

        public string Name => _name;
        public int Count => _questions.Count;

        public QuizBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public QuizBuilder AddQuestion(string text, int points, object answer, int? position = null)
        {
            var question = new QuestionData()
                {Text = text, Points = points, Answer = answer, Type = GetCreationType(answer)};

            if (position == null || position >= this._questions.Count) this._questions.Add(question);
            else this._questions[(int)position] = question;

            return this;
        }

        public QuestionData GetQuestion(int i)
        {
            return _questions[i];
        }

        public QuizBuilder RemoveQuestion(int i)
        {
            _questions.RemoveAt(i);
            return this;
        }

        public Quiz Build()
        {
            var questions = _questions.Select(MapQuestionData).ToList();
            return new Quiz(_name, questions);
        }
    }
}