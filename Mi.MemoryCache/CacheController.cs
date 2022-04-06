using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mi.MemoryCache
{
    public interface ICacheController
    {
        string Get(string key);
        void Create(string key, string data);
        void Remove(string key);
        void RemoveAll();
    }
    public class CacheController : ICacheController
    {
        private IMemoryCache _memory;
        public CacheController(IMemoryCache memory)
        {
            _memory = memory;
        }
        public void Create(string key, string data)
        {
            string t;
            if (!_memory.TryGetValue<string>(key, out t))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
                _memory.Set<string>(key, data, cacheEntryOptions);
            }
            //throw new NotImplementedException();
        }

        public string Get(string key)
        {
            string t;
            var checker = _memory.TryGetValue<string>(key, out t);
            return t;

        }

        public void Remove(string key)
        {
            string t;
            if (_memory.TryGetValue<string>(key, out t))
                _memory.Remove(key);
            //throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            _memory.Dispose();
        }
    }
}
