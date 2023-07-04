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
            return await _DataStore.SaveItemAsync(item);
        }

        public async Task<bool> AddItemsAsync(IEnumerable<T> items)
        {
            return await _DataStore.SaveAllItemsAsync(items);
        }

        public async Task<T> GetItemByIdAsync(string id)
        {
            var result = await GetAllItemsAsync();
            var items = result.ToList();

            return items.FirstOrDefault(item => (item as IModel).Id == id);
        }
        public async Task<T> GetItemAsync(T item)
        {
            return await GetItemByIdAsync((item as IModel).Id);
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync(bool forceRefresh = false)
        {
            return await _DataStore.LoadAllItemsAsync();
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            var isUpdated = false;
            var result = await GetAllItemsAsync();
            var items = result.ToList();

            var oldItem = items.FirstOrDefault(i => (i as IModel).Id == (item as IModel).Id);
            if(oldItem != null)
            {
                items.Remove(oldItem);
                items.Add(item);
                isUpdated = await AddItemsAsync(items);
            }

            return isUpdated;
        }

        public async Task<bool> DeleteItemByIdAsync(string id)
        {
            var isDeleted = false;
            var result = await GetAllItemsAsync();
            var items = result.ToList();

            var item = items.FirstOrDefault(i => (i as IModel).Id == id);
            if(item != null)
            {
                items.Remove(item);
                isDeleted = await AddItemsAsync(items);
            }

            return isDeleted;
        }

        public async Task<bool> DeleteItemAsync(T item)
        {
            return await DeleteItemByIdAsync((item as IModel).Id);
        }

        public async Task<bool> DeleteAllItemsAsync(bool forceRefresh = false)
        {
            return await _DataStore.SaveAllItemsAsync(new List<T>());
        }
    }
}
