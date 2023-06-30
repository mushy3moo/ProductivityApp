using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> SaveItemAsync(T item);
        Task<bool> SaveItemsAsync(List<T> items);
        Task<T> LoadItemAsync(T item);
        Task<IEnumerable<T>> LoadItemsAsync();
    }
}
