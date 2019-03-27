using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Questions.Application.Quizes;

namespace Questions.Data
{
    public class FileLoaderReader
    {
        public static FileLoaderReader GetReader(StorageFile file)
        {
            return new FileLoaderReader(file);
        }

        private readonly StorageFile _file;
        private List<string> _lines;
        private int _position;

        private string ReadString()
        {
            return _lines[_position++];
        }

        private int ReadNumber()
        {
            return int.Parse(ReadString());
        }

        private Dictionary<string, bool> LoadOptions()
        {
            var options = new Dictionary<string, bool>();
            int ops = ReadNumber();

            for (int i = 0; i < ops; i++)
            {
                var option = ReadString().Split(FileLoader.SEPARATOR.ToCharArray());
                options[option[0]] = option[1] == "1";
            }

            return options;
        }

        private object LoadAnswer()
        {
            switch (QuestionTypeConverter.ToType(ReadNumber()))
            {
                case QuestionType.Text:
                    return ReadString();
                case QuestionType.TrueFalse:
                    return ReadNumber() == 1;
                default:
                    return LoadOptions();
            }
        }

        private Quiz LoadQuiz()
        {
            var builder = QuizBuilder.Create(ReadString());
            int questions = ReadNumber();

            for(int i=0;i<questions;i++) builder.AddQuestion(ReadString(), ReadNumber(), LoadAnswer());

            return builder.Build();
        }

        private FileLoaderReader(StorageFile file)
        {
            _file = file;
            _position = 0;
        }

        public async Task<List<Quiz>> Load()
        {
            _lines = (await FileIO.ReadLinesAsync(_file)).ToList();
            var quizes = new List<Quiz>();

            if (_lines.Count == 0) return quizes;

            int q = ReadNumber();
            for(int i=0;i<q;i++) quizes.Add(LoadQuiz());
            return quizes;
        }
    }
}