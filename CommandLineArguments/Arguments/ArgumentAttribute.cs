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

        public string DefaultValue { get; private set; }

        public string ExampleValue { get; private set; }

        public string LongName { get; private set; }

        public string ShortName { get; private set; }

        public string Description { get; private set; }
    }
}
