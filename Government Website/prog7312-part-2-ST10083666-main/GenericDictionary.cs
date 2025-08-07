using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POEPART1_ST10083666
{
    //The following code was adapted from geeksforgeeks
    // https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/
    //geeksforgeeks

    public class GenericDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, List<TValue>> dictionary;

        public GenericDictionary()
        {
            dictionary = new Dictionary<TKey, List<TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = new List<TValue>();
            }
            dictionary[key].Add(value);
        }

        public List<TValue> Get(TKey key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : new List<TValue>();
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public IEnumerable<TKey> GetKeys()
        {
            return dictionary.Keys;
        }
    }
}