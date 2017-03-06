using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arguments
{
    /// <summary>
    /// The minimum interface an argument must expose to be parsed.
    /// </summary>
    /// <typeparam name="T">The type of the value used.</typeparam>
    public interface IArgument<T>
    {
        /// <summary>
        /// The default value for the associated field.
        /// </summary>
        T DefaultValue { get; }

        /// <summary>
        /// An example value for the associated field.
        /// </summary>
        T ExampleValue { get; }

        /// <summary>
        /// The long name by which the field is exposed as an argument.
        /// </summary>
        string LongName { get; }

        /// <summary>
        /// The short name by which the field is exposed as an argument.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// A brief description of what the associated field does or is used for.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// When multiple supplied arguments with the same long or short name exist, the 0-indexed 
        /// location within the supplied arguments list the field should take the value of. A position of -1 means
        /// the attribute does not take a user-supplied argument.
        /// </summary>
        /// <example>
        /// For an example argument "--Argument A B C", the position of A is 0, B is 1, and C is 2. Or, if the usage
        /// is "--Argument", the position is -1.
        /// </example>
        int Position { get; }
    }
}
