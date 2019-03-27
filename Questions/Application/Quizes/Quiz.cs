using System.Collections.Generic;
using System.Diagnostics;
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

        // TODO: Remove (Debugging Only)
        public void Info()
        {
            foreach (var q in _questions)
            {
                Debug.WriteLine(q.Text);
                Debug.WriteLine(q.Points);

                if (q is OptionsQuestion op)
                {
                    op.Options.ForEach(option => Debug.WriteLine(option));
                }
            }
        }
    }
}