using Newtonsoft.Json;
using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProductivityApp.Services
{
    public class LocalDataService<T> : IDataService<T>
    {
        private readonly string DataPath;

        public LocalDataService()
        {
            DataPath = SetAppDataPath();
            CreateAppDirectory(DataPath);
        }

        public async Task<bool> AddItemAsync(T item)
        {
            await SaveItemAsync(item, DataPath);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemsAsync(List<T> items)
        {
            await SaveItemsAsync(items, DataPath);

            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(string id)
        {
            var result = await GetItemsAsync();
            var items = result.ToList();

            return items.FirstOrDefault(item => (item as IModel).Id == id);
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            return await LoadItemsAsync(DataPath);
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            var result = await GetItemsAsync();
            var items = result.ToList();
            var oldItem = await GetItemAsync((item as IModel).Id);
            items.Remove(oldItem);
            items.Add(item);
            await AddItemsAsync(items);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var result = await GetItemsAsync();
            var items = result.ToList();
            var item = await GetItemAsync(id);
            items.Remove(item);
            await AddItemsAsync(items);

            return await Task.FromResult(true);
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

        private async Task SaveItemAsync(T item, string dataPath)
        {
            if (!Directory.Exists(dataPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + dataPath);
            }
            var json = JsonConvert.SerializeObject(item);
            var fullPath = Path.Combine(dataPath, $"{nameof(T)}.json");
            using (StreamWriter writer = new StreamWriter(fullPath, true)) 
            {
                await writer.WriteAsync(json);
            }
        }

        private async Task SaveItemsAsync(List<T> items, string dataPath)
        {
            if (!Directory.Exists(dataPath))
            {
                throw new DirectoryNotFoundException("The specified directory does not exist: " + dataPath);
            }

            var json = JsonConvert.SerializeObject(items);
            var fullPath = Path.Combine(dataPath, $"{nameof(T)}.json");
            using (StreamWriter writer = new StreamWriter(fullPath, false)) 
            {
                await writer.WriteAsync(json);
            }
        }

        private async Task<IEnumerable<T>> LoadItemsAsync(string dataPath)
        {

            if (!Directory.Exists(dataPath))
            {
                throw new DirectoryNotFoundException($"The specified directory does not exist: {dataPath}");
            }

            var items = new List<T>();
            var fullPath = Path.Combine(dataPath, $"{nameof(T)}.json");

            if (File.Exists(fullPath))
            {
                using(StreamReader reader = new StreamReader(fullPath))
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
    }
}
