using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312_POEPART1_ST10083666
{
    public class GenericQueue<T>
    {
        //The following code was adapted from geeksforgeeks
        //https://www.geeksforgeeks.org/c-sharp-queue-class/
        //geeksforgeeks


        private readonly Queue<T> queue;

        public GenericQueue()
        {
            queue = new Queue<T>();
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
        }

        public T Dequeue()
        {
            return queue.Count > 0 ? queue.Dequeue() : default(T);
        }

        public T Peek()
        {
            return queue.Count > 0 ? queue.Peek() : default(T);
        }

        public int Count()
        {
            return queue.Count;
        }

        public bool Contains(T item)
        {
            return queue.Contains(item);
        }

        public List<T> Take(int count)
        {
            List<T> result = new List<T>();
            int itemsToTake = count < queue.Count ? count : queue.Count;
            for (int i = 0; i < itemsToTake; i++)
            {
                result.Add(queue.Dequeue());
            }
            return result;
        }
    }
}