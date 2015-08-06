using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeLibrary
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        class Element
        {
            public T value;
            public Element left;
            public Element right;
        }   

        private IComparer<T> comparer;
        private Element root;

        public BinaryTree() : this(null, null){}

        public BinaryTree(T[] values) : this(values, null) { }

        public BinaryTree(T[] values, IComparer<T> comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            Add(values);
        }

        public void Add(T value)
        {
            Add(value, ref root);
        }

        public void Add(T[] values)
        {
            if (values == null || values.Length < 1)
                throw new ArgumentNullException();
            foreach (T value in values)
            {
                Add(value, ref root);
            }
        }

        private void Add(T value, ref Element root)
        {
            if (root == null)
            {
                root = new Element();
                root.value = value;
                root.left = null;
                root.right = null;
            }
            else
            {
                if (comparer.Compare(value, root.value) <= 0)
                    Add(value, ref root.left);
                else
                    if (comparer.Compare(value, root.value) > 0)
                        Add(value, ref root.right);
            }
        }

        private IEnumerable<T> PreorderTraversal(Element root)
        {
            if (root == null) 
                yield break;
            yield return root.value;
            
            if (root.left != null)
            {
                PreorderTraversal(root.left);
            }
            if (root.right != null)
            {
                PreorderTraversal(root.right);
            }
        }

        private IEnumerable<T> InorderTraversal(Element root)
        {
            if (root.left != null)
            {
                InorderTraversal(root.left);
            }
            yield return root.value;

            if (root.right != null)
            {
                InorderTraversal(root.right);
            }
        }

        private IEnumerable<T> PostorderTraversal(Element root)
        {
            if (root.left != null)
            {
                PostorderTraversal(root.left);
            }

            if (root.right != null)
            {
                PostorderTraversal(root.right);
            }
            yield return root.value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PreorderTraversal(root).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
