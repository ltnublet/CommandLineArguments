namespace Drexel.Arguments
{
    /// <summary>
    /// The minimum interface an argument must expose to be parsed.
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// The long name by which the argument is exposed.
        /// </summary>
        string LongName { get; }

        /// <summary>
        /// The short name by which the argument is exposed.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// A brief description of what the argument does or is used for.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// An example of a valid value the argument could be.
        /// </summary>
        string ExampleValue { get; }
    }
}
