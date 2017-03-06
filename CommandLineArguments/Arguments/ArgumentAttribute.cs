using System;

namespace Drexel.Arguments
{
    /// <summary>
    /// Mark fields to be handled as command-line arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ArgumentAttribute : Attribute, IArgument<string>
    {
        /// <summary>
        /// Instantiates an <see cref="ArgumentAttribute"/> using the supplied parameters.
        /// </summary>
        /// <param name="defaultValue">The default value of the argument as a string parsable by that type.</param>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="exampleValue">An example of a valid value the argument could be.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="position">
        /// The argument's position, 0 indexed. Set to -1 if the argument does not require a command line value.
        /// Defaults to 0. For example, given "-Arguments A B C", A is position 0, B is position 1, etc. 
        /// If no command line value is required, the argument would take the form of "-Argument".
        /// </param>
        public ArgumentAttribute(
            string defaultValue, 
            string longName, 
            string shortName,
            string exampleValue,
            string description,
            int position = 0)
        {
            this.DefaultValue = defaultValue;
            this.ExampleValue = exampleValue;
            this.LongName = longName;
            this.ShortName = shortName;
            this.Description = description;
            this.Position = position;
        }

        /// <summary>
        /// The default value for the associated field. A parsable string for all non-string types.
        /// </summary>
        /// <example>
        /// For a field of type string, "An Example String".
        /// For a field of type double, "8675.309".
        /// For a field of type int, "8675309".
        /// </example>
        public string DefaultValue { get; private set; }

        /// <summary>
        /// An example value for the associated field. A parsable string for all non-string types.
        /// </summary>
        /// <example>
        /// See <see cref="DefaultValue"/> for examples.
        /// </example>
        public string ExampleValue { get; private set; }

        /// <summary>
        /// The long name by which the field is exposed as an argument.
        /// </summary>
        /// <example>
        /// If the associated field should be set using the argument "--ExampleParameter {value}", "ExampleParameter".
        /// </example>
        public string LongName { get; private set; }

        /// <summary>
        /// The short name by which the field is exposed as an argument.
        /// </summary>
        /// <example>
        /// If the associated field should be set using the argument "-ep {value}", "ep".
        /// </example>
        public string ShortName { get; private set; }

        /// <summary>
        /// A brief description of what the associated field does or is used for.
        /// </summary>
        /// <example>
        /// If the associated field is an interval measured in seconds between cache refreshes, 
        /// "Controls the cache refresh interval (seconds)".
        /// </example>
        public string Description { get; private set; }

        /// <summary>
        /// When multiple <see cref="ArgumentAttribute"/>s with the same long or short name exist, the 0-indexed 
        /// location within the supplied arguments list the field should take the value of. A position of -1 means
        /// the attribute does not take a user-supplied argument.
        /// </summary>
        /// <example>
        /// For an example argument "--Argument A B C", the position of A is 0, B is 1, and C is 2. Or, if the usage
        /// is "--Argument", the position is -1.
        /// </example>
        public int Position { get; private set; }
    }
}
