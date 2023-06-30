using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public interface IDataService<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> AddItemsAsync(List<T> item);
        Task<T> GetItemByIdAsync(string id);
        Task<IEnumerable<T>> GetItemsByIdAsync(bool forceRefresh = false);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemByIdAsync(string id);
    }
}
