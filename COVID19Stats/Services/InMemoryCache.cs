using BlazorStorage.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COVID19Stats.Services
{
    public class InMemoryCache
    {
        private readonly ILocalStorage _storage;
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public InMemoryCache(ILocalStorage storage)
        {
            _storage = storage;
        }

        public Task Set<T>(string key, T data)
        {
            _data.Add(key, data);
            return Task.CompletedTask;
        }

        public Task<T> Get<T>(string key)
        {
            if (!_data.ContainsKey(key))
            {
                return Task.FromResult(default(T));
            }

            return Task.FromResult((T)_data[key]);
        }
    }
}
