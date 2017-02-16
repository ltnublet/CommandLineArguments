using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Arguments
{
    /// <summary>
    /// Mark fields to be handled as command-line arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ArgumentAttribute : Attribute
    {
        /// <summary>
        /// Instantiates an <see cref="ArgumentAttribute"/> using the supplied parameters.
        /// </summary>
        /// <param name="defaultValue">The default value of the argument as a string parsable by that type.</param>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="exampleValue">An example of a valid value the argument could be.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        public ArgumentAttribute(
            string defaultValue, 
            string longName, 
            string shortName,
            string exampleValue,
            string description)
        {
            this.DefaultValue = defaultValue;
            this.ExampleValue = exampleValue;
            this.LongName = longName;
            this.ShortName = shortName;
            this.Description = description;
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
    }
}
