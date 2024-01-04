using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    class DoublyLinkedListNode<T>
    {
        public readonly T Value;

        public DoublyLinkedListNode<T> Previous;

        public DoublyLinkedListNode<T> Next;

        public DoublyLinkedListNode(T value)
        {
            Value = value;
        }
    }
    public class DoublyLinkedListEnum<T> : IEnumerator<T>
    {
        private DoublyLinkedList<T> List;

        int position = -1;

        public DoublyLinkedListEnum(DoublyLinkedList<T> list)
        {
            List = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < List.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return List.ElementAt(position);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private DoublyLinkedListNode<T> First;
        private DoublyLinkedListNode<T> Last;
        public int Length
        {
            get
            {
                var count = 0;
                for (var el = First; el != null; el = el.Next)
                {
                    count++;
                }
                return count;
            }
        }

        public void Add(T e)
        {
            var newElem = new DoublyLinkedListNode<T>(e);
            if (First is null)
            {
                First = newElem;
                Last = newElem;
                return;
            }
            Last.Next = newElem;
            newElem.Previous = Last;
            Last = newElem;
        }
        public void AddAt(int index, T e)
        {
            if (index < 0 || index > Length || (Length == 0 && index != 0))
            {
                throw new IndexOutOfRangeException();
            }
            if (index == Length)
            {
                Add(e);
                return;
            }
            var newElem = new DoublyLinkedListNode<T>(e);
            if (index == 0)
            {
                newElem.Next = First;
                First.Previous = newElem;
                First = newElem;
                return;
            }
            var elementBeforeIndex = GetAt(index - 1);
            var indexElement = elementBeforeIndex.Next;
            newElem.Next = indexElement.Next;
            indexElement.Previous = newElem;

            elementBeforeIndex.Next = newElem;
            newElem.Previous = elementBeforeIndex;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= Length || Length == 0)
            {
                throw new IndexOutOfRangeException();
            }

            return GetAt(index).Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnum<T>(this);
        }

        public void Remove(T item)
        {
            var element = First;
            for (var i = 0; i < Length; i++)
            {
                if (element.Value.Equals(item))
                {
                    RemoveAt(i);
                    return;
                }
                element = element.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Length || Length == 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (index == 0)
            {
                var value = First.Value;
                First.Next.Previous = null;
                First = First.Next;
                return value;
            }
            if (index == Length - 1)
            {
                var value = Last.Value;
                Last.Previous.Next = null;
                Last = Last.Previous;
                return value;
            }


            var elementBeforeIndex = GetAt(index - 1);
            var indexElement = elementBeforeIndex.Next;
            var elementAfterIndex = indexElement.Next;

            elementBeforeIndex.Next = indexElement.Next;
            elementAfterIndex.Previous = indexElement.Previous;

            return indexElement.Value;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private DoublyLinkedListNode<T> GetAt(int index)
        {
            var indexElement = First;
            for (int i = 0; i < index; i++)
            {
                indexElement = indexElement.Next;
            }

            return indexElement;
        }
    }
}
