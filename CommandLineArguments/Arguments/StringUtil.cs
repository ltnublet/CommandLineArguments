using System.Collections.Generic;
using System.Linq;

namespace Drexel.Arguments
{
    /// <summary>
    /// Contains some static methods pertaining to string parsing.
    /// </summary>
    internal static class StringUtil
    {
        /// <summary>
        /// If the supplied <paramref name="actual"/> starts with the supplied <paramref name="startsWith"/>, returns <paramref name="actual"/> without the leading <paramref name="startsWith"/>. Otherwise, returns <paramref name="actual"/>.
        /// </summary>
        /// <param name="actual">The string to perform the chop operation upon.</param>
        /// <param name="startsWith">The string to perform the chop operation with.</param>
        /// <returns><paramref name="actual"/> without the leading <paramref name="startsWith"/> if it started with <paramref name="startsWith"/>, or <paramref name="actual"/> otherwise.</returns>
        public static string Chop(string actual, string startsWith)
        {
            return actual.StartsWith(startsWith) ? actual.Substring(startsWith.Length) : actual;
        }

        /// <summary>
        /// If the supplied <paramref name="actual"/> starts with any of the supplied <paramref name="startsWith"/>s, returns <paramref name="actual"/> without the leading <paramref name="startsWith"/> element. Otherwise, returns <paramref name="actual"/>.
        /// </summary>
        /// <param name="actual">The string to perform the chop operation upon.</param>
        /// <param name="startsWith">The collection of strings to perform the chop operation with.</param>
        /// <returns><paramref name="actual"/> without the leading <paramref name="startsWith"/> if it started with any of those strings, or <paramref name="actual"/> otherwise.</returns>
        public static string Chop(string actual, IEnumerable<string> startsWith)
        {
            foreach (string result in startsWith.Select(x => StringUtil.Chop(actual, x)))
            {
                if (result != actual)
                {
                    return result;
                }
            }

            return actual;
        }
    }
}
