using System;
using System.Diagnostics;
using HashTableLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHashTable
{
    [TestClass]
    public class TestHashTable
    {
        [TestMethod]
        public void AddingValuesToHashTable()
        {
            HashTable<int, int> hashTable = new HashTable<int, int>(10);

            hashTable.Add(1, 1);
            hashTable.Add(11, 2);
            hashTable.Add(2, 2);
            hashTable.Add(3, 3);

            Assert.AreEqual(hashTable.Count, 4);
        }

        [TestMethod]
        public void HashTable_GetValue_Key_11_ExpectedValue_2()
        {
            HashTable<int, int> hashTable = new HashTable<int, int>(10);

            hashTable.Add(1, 1);
            hashTable.Add(11, 2);
            hashTable.Add(2, 2);
            hashTable.Add(3, 3);

            Assert.AreEqual(hashTable.GetValue(11), 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertValueByDuplicatedKey_ExpectedException()
        {
            HashTable<int, int> hashTable = new HashTable<int, int>(10);

            hashTable.Add(1, 1);
            hashTable.Add(1, 2);
        }

        [TestMethod]
        public void EnumeratorTest()
        {
            HashTable<int, int> hashTable = new HashTable<int, int>(10);

            hashTable.Add(1, 1);
            hashTable.Add(11, 2);
            hashTable.Add(2, 2);
            hashTable.Add(3, 3);

            foreach (var pair in hashTable)
            {
                Debug.WriteLine(pair.Value);
            }
        }
    }
}
