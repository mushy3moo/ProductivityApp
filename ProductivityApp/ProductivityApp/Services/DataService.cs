using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public class DataService<T> : IDataService<T>
    {
        private readonly IDataStore<T> _DataStore;

        public DataService(IDataStore<T> dataStore)
        {
            _DataStore = dataStore;
        }

        public async Task<bool> AddItemAsync(T item)
        {
            await _DataStore.SaveItemAsync(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemsAsync(List<T> items)
        {
            await _DataStore.SaveItemsAsync(items);

            return await Task.FromResult(true);
        }

        public async Task<T> GetItemByIdAsync(string id)
        {
            var result = await GetItemsByIdAsync();
            var items = result.ToList();

            return items.FirstOrDefault(item => (item as IModel).Id == id);
        }

        public async Task<IEnumerable<T>> GetItemsByIdAsync(bool forceRefresh = false)
        {
            return await _DataStore.LoadItemsAsync();
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            var result = await GetItemsByIdAsync();
            var items = result.ToList();
            var oldItem = await GetItemByIdAsync((item as IModel).Id);
            items.Remove(oldItem);
            items.Add(item);
            await AddItemsAsync(items);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemByIdAsync(string id)
        {
            var result = await GetItemsByIdAsync();
            var items = result.ToList();
            var item = await GetItemByIdAsync(id);
            items.Remove(item);
            await AddItemsAsync(items);

            return await Task.FromResult(true);
        }
    }
}
