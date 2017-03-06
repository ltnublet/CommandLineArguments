using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arguments.Collections
{
    /// <summary>
    /// Represents a tree of <see cref="TreeNode{T}"/>s.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="TreeNode{T}"/> contained by the tree.</typeparam>
    internal class Tree<T> : IEnumerable<T>
    {
        /// <summary>
        /// Instantiates a new <see cref="Tree{T}"/> using the supplied <see cref="TreeNode{T}"/> as the root.
        /// </summary>
        /// <param name="root">The root of the tree. Must be non-null.</param>
        public Tree(TreeNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root), "Instances of this class must implement a non-null root.");
            }

            this.Root = root;
        }

        /// <summary>
        /// The root of the tree.
        /// </summary>
        public TreeNode<T> Root { get; private set; }

        /// <summary>
        /// Returns an <see cref="IEnumerator{out T}"/> which performs a depth-first flattening of the associated <see cref="Tree{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> where the type is determined by the <see cref="Tree{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Root.GetEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator{out T}"/> which performs a depth-first flattening of the associated <see cref="Tree{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{T}"/> where the type is determined by the <see cref="Tree{T}"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
