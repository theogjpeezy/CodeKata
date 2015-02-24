using System.Collections.Generic;

namespace DictionaryRepository
{
    public class DictionaryStorage<T>
    {
        private readonly Dictionary<string, T> _dictionary;

		public DictionaryStorage()
		{
			_dictionary = new Dictionary<string, T>();
		}

	    public IEnumerable<string> GetKeys() {
	       return _dictionary.Keys;
	    }

		public T Get(string key)
		{
			return _dictionary[key];
		}

		public void Set(string key, T value)
		{
			_dictionary[key] = value;
		}

        public bool Exists(string key) {
            return _dictionary.ContainsKey(key);
        }
    }
}
