using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drexel.Arguments
{
    /// <summary>
    /// Represents a manually-handled user-argument which performs runtime mutation of other-scoped variables.
    /// </summary>
    public class ManualArgument : IArgument
    {
        /// <summary>
        /// The action to perform upon the supplied values for this argument.
        /// </summary>
        protected Action<IEnumerable<string>> argumentAction;

        /// <summary>
        /// Special action which occurs only when a flag argument is not supplied.
        /// </summary>
        protected Action missingAction;

        /// <summary>
        /// The number of supplied values required to perform the <see cref="ManualArgument.argumentAction"/>.
        /// </summary>
        protected int argumentCount;

        /// <summary>
        /// Instantiates a <see cref="ManualArgument"/> using the supplied parameters.
        /// </summary>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="exampleValue">An example of a valid value the argument could be.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="argumentCount">The number of sequential following inputs this argument accepts.</param>
        /// <param name="action">Performs some runtime mutation of other-scoped variables based on the arguments.</param>
        public ManualArgument(
            string longName, 
            string shortName, 
            string exampleValue, 
            string description,
            int argumentCount, 
            Action<IEnumerable<string>> action)
        {
            this.LongName = longName;
            this.ShortName = shortName;
            this.Description = description;
            this.ExampleValue = exampleValue;
            this.argumentCount = argumentCount;
            this.argumentAction = action;
            this.missingAction = null;
        }

        /// <summary>
        /// Instantiates a <see cref="ManualArgument"/> using the supplied parameter.
        /// </summary>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="exampleValue">An example of a valid value the argument could be.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="action">Performs some runtime mutation of other-scoped variables based on the argument.</param>
        public ManualArgument(
            string longName,
            string shortName,
            string exampleValue,
            string description,
            Action<string> action) 
                : this(
                      longName, 
                      shortName,
                      exampleValue, 
                      description, 
                      1, 
                      x => action(x?.FirstOrDefault()))
        {
        }

        /// <summary>
        /// Instantiates a <see cref="ManualArgument"/> which operates as a flag.
        /// </summary>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="action">Performs some runtime mutation of other-scoped variables when the flag is specified.</param>
        public ManualArgument(
            string longName,
            string shortName,
            string description,
            Action action) 
                : this(
                    longName,
                    shortName,
                    "False",
                    description,
                    0,
                    x => action())
        {
        }

        /// <summary>
        /// Instantiates a <see cref="ManualArgument"/> which operates as a flag, and which has defined behavior for when the associated argument is not present.
        /// </summary>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="actionWhenSupplied">Performs some runtime mutation of other-scoped variables when the flag is specified.</param>
        /// <param name="actionWhenMissing">Performs some runtime mutation of other-scoped variables when the flag is not specified.</param>
        public ManualArgument(
            string longName,
            string shortName,
            string description,
            Action actionWhenSupplied,
            Action actionWhenMissing) 
                : this(
                    longName,
                    shortName,
                    description,
                    actionWhenSupplied)
        {
            this.missingAction = actionWhenMissing;
        }
        
        /// <summary>
        /// Instantiates a <see cref="ManualArgument"/> using the supplied parameter, but with additional logic based on whether the user supplied the associated longName or shortName at runtime.
        /// </summary>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="exampleValue">An example of a valid value the argument could be.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="actionWhenSupplied">Performs some runtime mutation of other-scoped variables based on the argument.</param>
        /// <param name="actionWhenMissing">Performs some runtime mutation of other-scoped variables, but only when the argument is not supplied.</param>
        /// <example>
        /// ManualArgument myArg = 
        ///     new ManualArgument(
        ///         "longName", 
        ///         "shortName", 
        ///         "exampleValue",
        ///         "description", 
        ///         arg => throw new NotImplementedException(), 
        ///         () => throw new InvalidOperationException());
        /// For this example argument, if the user runs the program with the argument "longName" or "shortName", a NotImplementedException will be thrown.
        /// However, if the user does not run the program with the argument "longName" or "shortName", an InvalidOperationException will be thrown.
        /// This example shows how the <paramref name="actionWhenSupplied"/> and <paramref name="actionWhenMissing"/> are affected by the user supplying or not supplying the associated argument to the program.
        /// </example>
        public ManualArgument(
            string longName,
            string shortName,
            string exampleValue,
            string description,
            Action<string> actionWhenSupplied,
            Action actionWhenMissing)
                : this(
                      longName,
                      shortName,
                      exampleValue,
                      description,
                      actionWhenSupplied)
        {
            this.missingAction = actionWhenMissing;
        }

        /// <summary>
        /// Instantiates a <see cref="ManualArgument"/> using the supplied parameters, but with additional logic based on whether the user supplied the associated longName or shortName at runtime.
        /// </summary>
        /// <param name="longName">The long name by which to expose the argument; for example, TimeoutLength.</param>
        /// <param name="shortName">The short name by which to expose the argument; for example, t.</param>
        /// <param name="exampleValue">An example of a valid value the argument could be.</param>
        /// <param name="description">A brief description of what the argument does or is used for.</param>
        /// <param name="argumentCount">The number of sequential following inputs this argument accepts.</param>
        /// <param name="actionWhenSupplied">Performs some runtime mutation of other-scoped variables based on the arguments.</param>
        /// <param name="actionWhenMissing">Performs some runtime mutation of other-scoped variables, but only when the argument is not supplied.</param>
        /// <example>
        /// ManualArgument myArg = 
        ///     new ManualArgument(
        ///         "longName", 
        ///         "shortName", 
        ///         "exampleValue",
        ///         "description",
        ///         3,
        ///         arg => throw new NotImplementedException(string.Join(", ", arg)), 
        ///         () => throw new InvalidOperationException());
        /// For this example argument, if the user runs the program with the argument "longName" or "shortName", a NotImplementedException will be thrown where the exception message contains the values supplied for the argument.
        /// However, if the user does not run the program with the argument "longName" or "shortName", an InvalidOperationException will be thrown.
        /// This example shows how the <paramref name="actionWhenSupplied"/> and <paramref name="actionWhenMissing"/> are affected by the user supplying or not supplying the associated argument to the program.
        /// </example>
        public ManualArgument(
            string longName,
            string shortName,
            string exampleValue,
            string description,
            int argumentCount,
            Action<IEnumerable<string>> actionWhenSupplied,
            Action actionWhenMissing)
                : this(
                      longName,
                      shortName,
                      exampleValue,
                      description,
                      argumentCount,
                      actionWhenSupplied)
        {
            this.missingAction = actionWhenMissing;
        }

        /// <summary>
        /// The long name by which the argument is exposed.
        /// </summary>
        public string LongName { get; protected set; }

        /// <summary>
        /// The short name by which the argument is exposed.
        /// </summary>
        public string ShortName { get; protected set; }

        /// <summary>
        /// A brief description of what the argument does or is used for.
        /// </summary>
        public string Description { get; protected set; }
        
        /// <summary>
        /// An example of a valid value the argument could be.
        /// </summary>
        public string ExampleValue { get; protected set; }

        /// <summary>
        /// Invokes the <see cref="ManualArgument.argumentAction"/>.
        /// </summary>
        public void InvokeSupplied(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            if (args.Length != this.argumentCount)
            {
                throw new ArgumentException("Arguments collection must match expected length.", nameof(args));
            }

            this.argumentAction(args);
        }

        /// <summary>
        /// Invokes the <see cref="ManualArgument.missingAction"/>. Unless this <see cref="ManualArgument"/> is a flag which was not supplied, this will have no effect.
        /// </summary>
        public void InvokeMissing()
        {
            this.missingAction?.Invoke();
        }
    }
}
