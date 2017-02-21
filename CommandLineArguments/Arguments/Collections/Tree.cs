using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arguments.Collections
{
    internal class Tree<T> : IEnumerable<T>
    {
        public Tree(TreeNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root), "Instances of this class must implement a non-null root.");
            }

            this.Root = root;
        }

        public TreeNode<T> Root { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
