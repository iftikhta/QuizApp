using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Questions.Application.Quizes;

namespace Questions.Data
{
    public class FileLoaderWriter
    {
        public static FileLoaderWriter GetWriter(StorageFile file, List<Quiz> quizes)
        {
            return new FileLoaderWriter(file, quizes);
        }

        private readonly StorageFile _file;
        private readonly List<Quiz> _quizes;
        private readonly List<string> _lines;

        private void WriteLine(object s = null)
        {
            _lines.Add(s != null ? s.ToString() : "");
        }

        private void SaveOptions(Dictionary<string, bool> options)
        {
            WriteLine(options.Count);

            foreach (var option in options.Keys)
            {
                var state = options[option] ? '1' : '0';
                WriteLine($"{option}{FileLoader.SEPARATOR}{state}");
            }
        }

        private void SaveQuestion(QuestionItem question)
        {
            WriteLine(question.Text);
            WriteLine(question.Points);
            WriteLine(QuestionTypeConverter.ToValue(question.Type));

            switch (question.Type)
            {
                case QuestionType.Text:
                    WriteLine(question.Answer.ToString());
                    break;
                case QuestionType.TrueFalse:
                    WriteLine((bool) question.Answer ? "1" : "0");
                    break;
                default:
                    SaveOptions((Dictionary<string, bool>) question.Answer);
                    break;
            }
        }

        private void SaveQuiz(Quiz quiz)
        {
            WriteLine(quiz.Name);
            WriteLine(quiz.Count);

            for (int i = 0; i < quiz.Count; i++)
                SaveQuestion(quiz[i]);
        }

        private FileLoaderWriter(StorageFile file, List<Quiz> quizes)
        {
            _file = file;
            _quizes = quizes;
            _lines = new List<string>();
        }

        public async Task Save()
        {
            WriteLine(_quizes.Count);

            foreach (var quiz in _quizes) SaveQuiz(quiz);

            await FileIO.AppendLinesAsync(_file, _lines);
        }
    }
}