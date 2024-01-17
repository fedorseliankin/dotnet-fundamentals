using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        public DoublyLinkedList<T> Storage;
        public HybridFlowProcessor()
        {
            Storage = new DoublyLinkedList<T>();
        }

        public T Dequeue()
        {
            if (Storage.Length <= 0)
            {
                throw new InvalidOperationException();
            }

            return Storage.RemoveAt(0);

        }

        public void Enqueue(T item)
        {
            Storage.Add(item);
        }

        public T Pop()
        {
            if (Storage.Length <= 0)
            {
                throw new InvalidOperationException();
            }

            return Storage.RemoveAt(Storage.Length - 1);

        }

        public void Push(T item)
        {
            Storage.Add(item);
        }
    }
}
