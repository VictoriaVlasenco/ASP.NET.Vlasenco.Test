using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableLibrary
{
    public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private class Entry
        {
            public int HashCode { get; set; }         
            public TKey Key { get; set; }          
            public TValue Value { get; set; }
            public Entry Next; 

            public Entry() { }

            public Entry(int hashcode, TKey key, TValue value) : this(hashcode, key, value, null) { }

            public Entry(int hashcode, TKey key, TValue value, Entry next)
            {
                HashCode = hashcode;
                Key = key;
                Value = value;
                Next = next;
            }
        }

        private Entry[] buckets;
        private IEqualityComparer<TKey> comparer;
        private Func<TKey, int> hashMethod; 

        public HashTable(int capacity) : this(capacity, null as IEqualityComparer<TKey>) { }

        public HashTable(int capacity, Func<TKey, int> hashMethod)
            : this(capacity, null as IEqualityComparer<TKey>)
        {
            this.hashMethod = hashMethod;
        }

        public HashTable(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0) 
                throw new ArgumentOutOfRangeException("Capacity must be positive");
            if (capacity > 0) buckets = new Entry[capacity];
            this.comparer = comparer ?? EqualityComparer<TKey>.Default;
        }

        public int Count { get; private set; }

        public void Add(TKey key, TValue value)
        {
            Insert(key, value);
        }

        public KeyValuePair<TKey, TValue> Get(TKey key)
        {
            return FindEntry(key);
        }

        public TValue GetValue(TKey key)
        {
            return FindEntry(key).Value;
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                Entry item = buckets[i];
                while (item != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
                    item = item.Next;
                }

            }
        }

        private void Insert(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException();
            int hashCode = GetHashCode(key);
            int targetBucket = hashCode % buckets.Length;

            var entry = new Entry(hashCode, key, value);
            Add(entry, ref buckets[targetBucket]);
            Count++;
        }

        private void Add(Entry entry, ref Entry listItem)
        {
            if (listItem == null)
            {
                listItem = entry;
            }
            else
            {
                if (entry.HashCode == listItem.HashCode && comparer.Equals(entry.Key, listItem.Key))
                    throw new ArgumentException();
                Add(entry, ref listItem.Next);
            }
        }

        private KeyValuePair<TKey, TValue> FindEntry(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentException();
            }

            if (buckets != null)
            {
                int hashCode = GetHashCode(key);
                
                foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
                {
                    int itemHashCode = GetHashCode(keyValuePair.Key);
                    if (itemHashCode == hashCode && comparer.Equals(keyValuePair.Key, key)) return keyValuePair;
                }
            }
            return new KeyValuePair<TKey, TValue>(default(TKey), default(TValue));
        }

        private int GetHashCode(TKey key)
        {
            if (hashMethod != null) 
                return hashMethod(key);
            return comparer.GetHashCode(key) & 0x7FFFFFFF;
        }
    }
}
