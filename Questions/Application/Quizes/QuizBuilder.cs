using Questions.Application.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Media.Protection.PlayReady;

namespace Questions.Application.Quizes
{
    public class QuizBuilder
    {
        public static QuizBuilder Create(string name = "")
        {
            return new QuizBuilder(name);
        }

        private string _name;
        private readonly List<QuestionItem> _questions;

        private static QuestionType GetCreationType(object value)
        {
            if (value is string) return QuestionType.Text;
            if (value is bool) return QuestionType.TrueFalse;
            return QuestionType.Options;
        }

        private QuizBuilder(string name)
        {
            _name = name;
            _questions = new List<QuestionItem>();
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
            var question = new QuestionItem()
                {Text = text, Points = points, Answer = answer, Type = GetCreationType(answer)};

            if (position == null || position >= this._questions.Count) this._questions.Add(question);
            else this._questions[(int)position] = question;

            return this;
        }

        public QuestionItem GetQuestion(int i)
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
            return new Quiz(_name, _questions);
        }
    }
}