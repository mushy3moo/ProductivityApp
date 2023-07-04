using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityApp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> SaveItemAsync(T item);
        Task<bool> SaveAllItemsAsync(IEnumerable<T> items);
        Task<IEnumerable<T>> LoadAllItemsAsync();
    }
}
