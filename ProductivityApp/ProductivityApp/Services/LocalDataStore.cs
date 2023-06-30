using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductivityApp.Services
{
    public class LocalDataStore<T> : IDataStore<T>
    {
        private readonly string DataPath;

        public LocalDataStore()
        {
            DataPath = SetAppDataPath();
            CreateAppDirectory(DataPath);
        }

        private string SetAppDataPath()
        {
            var directoryPath = string.Empty;
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    break;
                case Device.iOS:
                    directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "ApplicationSupport", "ProductivityApp");
                    break;
                case Device.macOS:
                    directoryPath = Path.Combine("/Applications", "ProductivityApp");
                    break;
                case Device.UWP:
                    directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProductivityApp");
                    break;
                default:
                    var errorMessage = "Device Operating System is not compatible.";
                    throw new Exception(errorMessage);
            }
            return Path.Combine(directoryPath, "data");
        }

        private void CreateAppDirectory(string dataPath)
        {
            var directoryPath = Path.GetDirectoryName(dataPath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }

        public Task<T> LoadItemAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> LoadItemsAsync()
        {

            if (!Directory.Exists(DataPath))
            {
                throw new DirectoryNotFoundException($"The specified directory does not exist: {DataPath}");
            }

            var items = new List<T>();
            var fullPath = Path.Combine(DataPath, $"{nameof(T)}.json");

            if (File.Exists(fullPath))
            {
                using (var reader = new StreamReader(fullPath))
                {
                    var json = await reader.ReadToEndAsync();
                    items = JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            else
            {
                throw new FileNotFoundException($"The specified file does not exist: {fullPath}");
            }

            return items;
        }

        public async Task<bool> SaveItemAsync(T item)
        {
            if (!Directory.Exists(DataPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + DataPath);
            }
            var json = JsonConvert.SerializeObject(item);
            var fullPath = Path.Combine(DataPath, $"{nameof(T)}.json");
            using (var writer = new StreamWriter(fullPath, true))
            {
                await writer.WriteAsync(json);
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> SaveItemsAsync(List<T> items)
        {
            if (!Directory.Exists(DataPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + DataPath);
            }

            var json = JsonConvert.SerializeObject(items);
            var fullPath = Path.Combine(DataPath, $"{nameof(T)}.json");
            using (var writer = new StreamWriter(fullPath, false))
            {
                await writer.WriteAsync(json);
            }

            return await Task.FromResult(true);
        }
    }
}
