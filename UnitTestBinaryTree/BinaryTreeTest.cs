using System;
using System.Diagnostics;
using BinaryTreeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestBinaryTree
{
    [TestClass]
    public class BinaryTreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            BinaryTree<int> tree = new BinaryTree<int>(new int[5] {5, 2, 7, 3, 4});
            foreach (int i in tree)
            {
                Debug.WriteLine(i);
            }
        }
    }
}
