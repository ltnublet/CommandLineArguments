////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;

////namespace Drexel.Arguments.Sets
////{
////    /// <summary>
////    /// Represents a runtime-determined <see cref="ArgumentAttribute"/> equivalent.
////    /// </summary>
////    public class ArgumentSet<T> : IArgument<T>
////    {
////        /// <summary>
////        /// When invoked, returns the default value for this argument.
////        /// </summary>
////        protected Func<T> defaultValueSource;

////        /// <summary>
////        /// When invoked, performs a conversion from a user-supplied string to the expected value for this argument.
////        /// </summary>
////        protected Func<string, T> actualValueSource;

////        /// <summary>
////        /// Instantiates an <see cref="ArgumentSet{T}"/> object using the supplied parameters.
////        /// </summary>
////        /// <param name="shortName">The short name by which the field is exposed as an argument.</param>
////        /// <param name="longName">The long name by which the field is exposed as an argument.</param>
////        /// <param name="description">A brief description of what the associated field does or is used for.</param>
////        /// <param name="position">
////        /// When multiple supplied arguments with the same long or short name exist, the 0-indexed 
////        /// location within the supplied arguments list the field should take the value of. A position of -1 means
////        /// the attribute does not take a user-supplied argument.
////        /// </param>
////        /// <param name="exampleValue">An example value for the associated field.</param>
////        /// <param name="defaultValue">The default value for the associated field.</param>
////        /// <param name="actualValue">When invoked, performs a conversion from a user-supplied string to the expected value for this argument.</param>
////        public ArgumentSet(
////            string shortName, 
////            string longName, 
////            string description, 
////            int position,
////            T exampleValue,
////            T defaultValue,
////            Func<string, T> actualValue)
////        {
////            this.actualValueSource = actualValue;
////            this.defaultValueSource = () => defaultValue;
////            this.ShortName = shortName;
////            this.LongName = longName;
////            this.Description = description;
////            this.ExampleValue = exampleValue;
////            this.Position = position;
////        }

////        /// <summary>
////        /// Instantiates an <see cref="ArgumentSet{T}"/> object using the supplied parameters.
////        /// </summary>
////        /// <param name="shortName">The short name by which the field is exposed as an argument.</param>
////        /// <param name="longName">The long name by which the field is exposed as an argument.</param>
////        /// <param name="description">A brief description of what the associated field does or is used for.</param>
////        /// <param name="position">
////        /// When multiple supplied arguments with the same long or short name exist, the 0-indexed 
////        /// location within the supplied arguments list the field should take the value of. A position of -1 means
////        /// the attribute does not take a user-supplied argument.
////        /// </param>
////        /// <param name="exampleValue">An example value for the associated field.</param>
////        /// <param name="defaultValue">When invoked, returns the default value for this argument.</param>
////        /// <param name="actualValue">When invoked, performs a conversion from a user-supplied string to the expected value for this argument.</param>
////        public ArgumentSet(
////            string shortName, 
////            string longName, 
////            string description, 
////            int position, 
////            T exampleValue, 
////            Func<T> defaultValue, 
////            Func<string, T> actualValue)
////        {
////            this.defaultValueSource = defaultValue;
////            this.actualValueSource = actualValue;
////            this.ShortName = shortName;
////            this.LongName = longName;
////            this.Description = description;
////            this.ExampleValue = exampleValue;
////            this.Position = position;
////        }

////        /// <summary>
////        /// An example value for the associated field.
////        /// </summary>
////        public T ExampleValue { get; protected set; }

////        /// <summary>
////        /// The long name by which the field is exposed as an argument.
////        /// </summary>
////        public string LongName { get; protected set; }

////        /// <summary>
////        /// The short name by which the field is exposed as an argument.
////        /// </summary>
////        public string ShortName { get; protected set; }

////        /// <summary>
////        /// A brief description of what the associated field does or is used for.
////        /// </summary>
////        public string Description { get; protected set; }

////        /// <summary>
////        /// When multiple supplied arguments with the same long or short name exist, the 0-indexed 
////        /// location within the supplied arguments list the field should take the value of. A position of -1 means
////        /// the attribute does not take a user-supplied argument.
////        /// </summary>
////        /// <example>
////        /// For an example argument "--Argument A B C", the position of A is 0, B is 1, and C is 2. Or, if the usage
////        /// is "--Argument", the position is -1.
////        /// </example>
////        public int Position { get; protected set; }
////    }
////}
