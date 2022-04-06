using System;
using System.Collections.Generic;
using System.Text;

namespace Mi.RamCache
{
    public interface IUseMemoryCaching
    {
        string Get(string key);
        void Create(string key, string value);
        void Remove(string key);
    }

    public class UseMemoryCaching : IUseMemoryCaching
    {
        private IMemoryCache _memory;
        public UseMemoryCaching(IMemoryCache memory)
        {
            _memory = memory;
        }

        public void Create(string key, string value)
        {
            //Kiem tra da co cache nay chua
            object t;
            if(_memory.TryGetValue(key, out t))
            {
                _memory.Remove(key);
                _memory.CreateEntry(key).Value = t.ToString();
            }
            else
            {
                _memory.CreateEntry(key).Value = t.ToString();
            }
            
        }

        public string Get(string key)
        {
            object t;
            if(_memory.TryGetValue(key, out t))
            {
                return t.ToString();
            }
            return "";
        }

        public void Remove(string key)
        {
            _memory.Remove(key);
            //throw new NotImplementedException();
        }
    }
}
