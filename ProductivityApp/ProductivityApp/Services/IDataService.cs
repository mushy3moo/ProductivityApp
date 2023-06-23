using ProductivityApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public interface IDataService<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> AddItemsAsync(List<T> item);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
    }
}
