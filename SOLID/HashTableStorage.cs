using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTableRepository {

	public class HashTableStorage<T> {
	    private readonly Hashtable _hashtable;

		public HashTableStorage()
		{
			_hashtable = new Hashtable();
		}

	    public IEnumerable<string> GetKeys() {
	       return _hashtable.Keys.Cast<string>();
	    }

		public T Get(string key)
		{
			return (T)_hashtable[key];
		}

		public void Set(string key, T value)
		{
			_hashtable[key] = value;
		}

	    public bool Exists(string key) {
	        return _hashtable.ContainsKey(key);
	    }
	}
}
