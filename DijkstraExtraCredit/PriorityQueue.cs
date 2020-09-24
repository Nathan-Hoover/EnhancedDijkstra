using System.Collections.Generic;

namespace DijkstraExtraCredit
{
    // Adapted from https://www.dotnetlovers.com/article/231/priority-queue
    public class PriorityQueue<T>
    {
        private class Node
        {
            public int Priority { get; set; }
            public T Object { get; set; }
        }

        private List<Node> Queue { get; } = new List<Node>();
        private int HeapSize { get; set; } = -1;
        private bool IsMinPriorityQueue { get; }

        public int Count
        {
            get
            {
                return Queue.Count;
            }
        }

        // If min queue or max queue
        public PriorityQueue(bool isMinPriorityQueue = false)
        {
            IsMinPriorityQueue = isMinPriorityQueue;
        }

        // Enqueue the object with priority
        public void Enqueue(int priority, T obj)
        {
            var node = new Node
            {
                Priority = priority,
                Object = obj
            };

            Queue.Add(node);
            HeapSize++;
            //Maintaining heap
            if (IsMinPriorityQueue)
            {
                BuildHeapMin(HeapSize);
            }
            else
            {
                BuildHeapMax(HeapSize);
            }
        }

        // Dequeue the object
        public T Dequeue()
        {
            var returnVal = Queue[0].Object;

            Queue[0] = Queue[HeapSize];
            Queue.RemoveAt(HeapSize);
            HeapSize--;

            //Maintaining lowest or highest at root based on min or max queue
            if (IsMinPriorityQueue)
            {
                MinHeapify(0);
            }
            else
            {
                MaxHeapify(0);
            }

            return returnVal;
        }

        /// <summary>
        /// Maintain max heap
        /// </summary>
        /// <param name="i"></param>
        private void BuildHeapMax(int i)
        {
            while (i >= 0 && Queue[(i - 1) / 2].Priority < Queue[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        /// <summary>
        /// Maintain min heap
        /// </summary>
        /// <param name="i"></param>
        private void BuildHeapMin(int i)
        {
            while (i >= 0 && Queue[(i - 1) / 2].Priority > Queue[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        // Non Recursive MaxHeapify
        private void MaxHeapify(int i)
        {
            while (true)
            {
                var left = GetLeftChild(i);
                var right = GetRightChild(i);

                var heighst = i;

                if (left <= HeapSize && Queue[heighst].Priority < Queue[left].Priority) heighst = left;
                if (right <= HeapSize && Queue[heighst].Priority < Queue[right].Priority) heighst = right;

                if (heighst == i)
                {
                    return;
                }

                Swap(heighst, i);
                i = heighst;
            }
        }

        // Non Recursive MinHeapify
        private void MinHeapify(int i)
        {
            while (true)
            {
                var left = GetLeftChild(i);
                var right = GetRightChild(i);

                var lowest = i;

                if (left <= HeapSize && Queue[lowest].Priority > Queue[left].Priority)
                {
                    lowest = left;
                }

                if (right <= HeapSize && Queue[lowest].Priority > Queue[right].Priority)
                {
                    lowest = right;
                }

                if (lowest == i)
                {
                    return;
                }

                Swap(lowest, i);
                i = lowest;
            }
        }

        private void Swap(int i, int j)
        {
            var temp = Queue[i];
            Queue[i] = Queue[j];
            Queue[j] = temp;
        }

        private static int GetLeftChild(int i)
        {
            return i * 2 + 1;
        }

        private static int GetRightChild(int i)
        {
            return i * 2 + 2;
        }
    }
}