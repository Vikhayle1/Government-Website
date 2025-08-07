using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POEPART1_ST10083666
{

    //The following code was adapted from geeksforgeeks
    //https://www.geeksforgeeks.org/c-sharp-hashset-class/
    //geeksforgeeks
    public class GenericSet<T>
    {
        private readonly HashSet<T> set;

        public GenericSet()
        {
            set = new HashSet<T>();
        }

        public void Add(T item)
        {
            set.Add(item);
        }

        public bool Contains(T item)
        {
            return set.Contains(item);
        }
        public bool Remove(T item)
        {
            return set.Remove(item);
        }

        public IEnumerable<T> GetItems()
        {
            return set;
        }
    }
}