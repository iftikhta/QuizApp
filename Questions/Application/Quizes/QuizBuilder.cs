using System.Collections.Generic;
using System.Linq;
using Questions.Application.Questions;

namespace Questions.Application.Quizes
{
    public class QuizBuilder
    {
        private string _name;
        private readonly List<Question> _questions;

        public static QuizBuilder create(string name)
        {
            return new QuizBuilder(name);
        }

        private QuizBuilder(string name)
        {
            _name = name;
            _questions = new List<Question>();
        }

        public QuizBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public QuizBuilder AddTrueFalseQuestion(string text, int points, bool answer)
        {
            _questions.Add(new TrueFalseQuestion(answer, text, points));
            return this;
        }

        public QuizBuilder AddTextQuestion(string text, int points, string answer)
        {
            _questions.Add(new TextQuestion(answer, text, points));
            return this;
        }

        public QuizBuilder AddOptionsQuestion(string text, int points, Dictionary<string, bool> options)
        {
            var optionsList = options.Keys.ToList();
            List<int> correctList = new List<int>();

            for(int i=0;i< optionsList.Count;i++)
               if(options[optionsList[0]])
                   correctList.Add(i);

            _questions.Add(new OptionsQuestion(optionsList, correctList, text, points));
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