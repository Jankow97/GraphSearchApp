using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace GraphSearchApp.Helpers
{
    class PriorityQueueHeap<T>
    {
        private T[] array;
        private int n;

        PriorityQueueHeap()
        {
            array = new T[100001];
            n = 0;
        }

        PriorityQueueHeap(int size)
        {
            array = new T[size];
            n = 0;
        }

        public T GetTop() => array[1];

        public T GetLow()
        {
            if (n > 0) return array[n];
            else throw new ArgumentOutOfRangeException();
        }

        public T RemoveTop()
        {
            T toReturn = array[1];
            array[1] = array[n];
            n--;
            Heapify();
            return toReturn;
        }

        public void Heapify()
        {
            for (int i = 1; i < n;)
            {
                //if (array[i] > array[i * 2] || array[i] > array[i * 2 + 1]) // todo: comparer
                //{

                //}
            }
        }
    }
}
