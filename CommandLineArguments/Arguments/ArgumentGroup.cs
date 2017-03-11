using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.Arguments.Collections;

namespace Drexel.Arguments
{
    /// <summary>
    /// Represents a set of runtime-determined equivalents to an <see cref="ArgumentAttribute"/>.
    /// </summary>
    public class ArgumentGroup : IReadOnlyCollection<ManualArgument>
    {
        /// <summary>
        /// Instantiates a new <see cref="ArgumentGroup"/>.
        /// </summary>
        /// <param name="arguments">The set of <see cref="ManualArgument"/>s to use in this argument group.</param>
        public ArgumentGroup(params ManualArgument[] arguments)
        {
            this.Arguments = arguments;
            this.Count = arguments.Length;
        }

        /// <summary>
        /// The arguments contained by this <see cref="ArgumentGroup"/>.
        /// </summary>
        public IReadOnlyCollection<ManualArgument> Arguments { get; protected set; }

        /// <summary>
        /// The number of arguments this <see cref="ArgumentGroup"/> contains.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        public IEnumerator<ManualArgument> GetEnumerator()
        {
            return this.Arguments.GetEnumerator();
        }

        /// <summary>
        /// Checks if any <see cref="ManualArgument"/> contained by this group has a name that matches <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name for which to look for a matching argument.</param>
        /// <returns>True if a matching argument was contained by this group, and false otherwise.</returns>
        public bool ContainsArgument(string name)
        {
            return this.Any(x => x.LongName == name || x.ShortName == name);
        }

        /// <summary>
        /// Checks if any <see cref="ManualArgument"/> contained by this group has a signature that matches <paramref name="argument"/>.
        /// </summary>
        /// <param name="argument">The argument for which to look for a match.</param>
        /// <returns>True if a matching argument was contained by this group, and false otherwise.</returns>
        public bool ContainsArgument(IArgument argument)
        {
            return this.Any(x => 
                x.LongName == argument.LongName 
                || x.ShortName == argument.ShortName);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Invokes the <see cref="ManualArgument"/>s contained in <see cref="ArgumentGroup.Arguments"/> using the supplied <see cref="Tree{string}"/>.
        /// </summary>
        /// <param name="tree">The arguments to iterate through the ArgumentGroup with.</param>
        /// <exception cref="ArgumentException">
        /// Occurs when the <see cref="ArgumentGroup"/> does not contain a 
        /// <see cref="ManualArgument"/> with the same long or short name as one of the supplied arguments in 
        /// <paramref name="tree"/>.
        /// </exception>
        /// <returns>Any arguments for which a corresponding <see cref="ManualArgument.InvokeSupplied(string[])"/> was performed.</returns>
        internal IEnumerable<IArgument> Invoke(Tree<string> tree)
        {
            List<ManualArgument> arguments = this.Arguments.ToList();
            List<IArgument> handled = new List<IArgument>();

            foreach (TreeNode<string> node in tree.Root.Children)
            {
                ManualArgument argument = arguments
                    .Where(x => x.LongName == node.Value || x.ShortName == node.Value)
                    .FirstOrDefault();

                if (argument != null)
                {
                    argument.InvokeSupplied(node.Skip(1).ToArray());
                    arguments.Remove(argument);
                    handled.Add(argument);
                }
            }

            foreach (ManualArgument argument in arguments)
            {
                argument.InvokeMissing();
            }

            return handled;
        }
    }
}
