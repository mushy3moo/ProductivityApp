using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public interface IDataService<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> AddItemsAsync(IEnumerable<T> item);
        Task<T> GetItemAsync(T item);
        Task<T> GetItemByIdAsync(string id);
        Task<IEnumerable<T>> GetAllItemsAsync(bool forceRefresh = false);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemByIdAsync(string id);
        Task<bool> DeleteAllItemAsync(bool forceRefresh = false);
    }
}
