using ProductivityApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityAppTests.UnitTests.Fakes
{
    public class FakeDataStore<T> : IDataStore<T>
    {
        private List<T> Data { get; set; }

        public FakeDataStore(List<T> data)
        {
            Data = data;
        }

        public async Task<IEnumerable<T>> LoadAllItemsAsync()
        {
            return await Task.FromResult(Data);
        }

        public Task<bool> SaveAllItemsAsync(IEnumerable<T> items)
        {
            Data = items.ToList();
            return Task.FromResult(true);
        }

        public Task<bool> SaveItemAsync(T item)
        {
            Data.Add(item);
            return Task.FromResult(true);
        }
    }
}
