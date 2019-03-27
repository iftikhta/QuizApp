using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Questions.Application.Quizes;

namespace Questions.Data
{
    public class FileLoader : QuizLoader
    {
        private static string FILE_NAME = "quiz_saves.txt";

        public static string SEPARATOR = ":::::::::";

        private List<Quiz> _quizes;

        private async Task SaveData()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting);

            await FileLoaderWriter.GetWriter(file, _quizes).Save();
        }

        private async Task<List<Quiz>> LoadData()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await storageFolder.CreateFileAsync(FILE_NAME, CreationCollisionOption.OpenIfExists);

            return await FileLoaderReader.GetReader(file).Load();
        }

        public FileLoader()
        {
            _quizes = null;
        }

        public List<Quiz> Load()
        {
            if (_quizes == null) _quizes = LoadData().GetAwaiter().GetResult();
            return _quizes;
        }

        public void Save(List<Quiz> list)
        {
            _quizes = list;
            SaveData().GetAwaiter().GetResult();
        }
    }
}