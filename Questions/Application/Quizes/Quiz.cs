using System.Collections.Generic;
using System.Linq;
using Questions.Application.Questions;

namespace Questions.Application.Quizes
{
    public class Quiz
    {
        private readonly List<Question> _questions;

        public Quiz(string name, List<Question> questions)
        {
            Name = name;
            _questions = questions;
        }

        public string Name { get; }
        public int Count => _questions.Count;
        public int Points => _questions.Select(q => q.Points).Sum();

        public Question this[int index] => _questions[index];
    }
}