using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next{ get; set; }
    }

    public class DoubleLinkedList<T>
    {
        private IEqualityComparer<T> comparer;

        public Node<T> First { get; private set; }
        public Node<T> Last { get; private set; }
        public int Count { get; private set; }

        public DoubleLinkedList() : this(null){}
        
        public DoubleLinkedList(IEqualityComparer<T> comparer)
        {
            this.comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public void AddLast(T value)
        {
            if (First == null)
            {
                First = new Node<T> {Value = value};
                Last = First;
            }
            else
            {
                Last.Next = new Node<T> { Value = value };
                Last.Next.Previous = Last;
            }
        }

        public void AddFirst(T value)
        {
            if (First == null)
            {
                First = new Node<T> { Value = value };
                Last = First;
            }
            else
            {
                First.Previous = new Node<T> { Value = value };
                First.Previous.Next = First;
            }
        }

        public void RemoveLast()
        {
            if (First == null)
                throw new NullReferenceException();
            Last = Last.Previous;
            Last.Next = null;
        }

        public void RemoveFirst()
        {
            if (First == null)
                throw new NullReferenceException();
            First = First.Next;
            First.Previous = null;
        }

        public Node<T> Find(T item)
        {
            while (First != null)
            {
                if (comparer.Equals(First.Value, item))
                    return First;
                First = First.Next;
            }
            return default(Node<T>);
        }

        public bool Contains(T item)
        {
            while (First != null)
            {
                if (comparer.Equals(First.Value, item))
                    return true;
                First = First.Next;
            }
            return false;
        }
    }
}
