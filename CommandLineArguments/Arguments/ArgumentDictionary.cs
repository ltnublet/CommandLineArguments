using System.Collections.Generic;
using System.Linq;

namespace Drexel.Arguments
{
    /// <summary>
    /// Represents a collection of <see cref="AttributeField"/>s grouped by <see cref="AttributeField.Attr.LongName"/> and <see cref="AttributeField.Attr.ShortName"/>.
    /// </summary>
    internal class ArgumentDictionary
    {
        private IEnumerable<AttributeField> cachedValues;
        private Dictionary<string, List<AttributeField>> innerContainer;

        /// <summary>
        /// Instantiates a new <see cref="ArgumentDictionary"/>.
        /// </summary>
        public ArgumentDictionary()
        {
            this.innerContainer = new Dictionary<string, List<AttributeField>>();
            this.cachedValues = null;
        }
        
        /// <summary>
        /// An unsorted collection of all the <see cref="AttributeField"/>s contained in the <see cref="ArgumentDictionary"/>.
        /// </summary>
        public IEnumerable<AttributeField> Values
        {
            get
            {
                if (this.cachedValues == null)
                {
                    this.cachedValues = this.innerContainer.Values
                        .Select(x => x.OrderBy(y => y.Attr.Position))
                        .Select(x => x.AsEnumerable())
                        .Aggregate((x, y) => y.Concat(x))
                        .Distinct();
                }

                return this.cachedValues;
            }
        }

        /// <summary>
        /// Gets the group of <see cref="AttributeField"/>s with the corresponding <see cref="ArgumentAttribute.LongName"/> or <see cref="AttributeField.Attr.ShortName"/>.
        /// </summary>
        /// <param name="key">The <see cref="ArgumentAttribute.LongName"/> or <see cref="AttributeField.Attr.ShortName"/> to index on.</param>
        /// <returns>The corresponding group of <see cref="AttributeField"/>s, ordered by position.</returns>
        public IEnumerable<AttributeField> this[string key]
        {
            get
            {
                return this.innerContainer[key].OrderBy(x => x.Attr.Position);
            }
        }

        /// <summary>
        /// Adds the supplied <see cref="AttributeField"/>s to the <see cref="ArgumentDictionary"/>.
        /// </summary>
        /// <param name="attr">The <see cref="ArgumentAttribute"/> to use the <see cref="ArgumentAttribute.LongName"/> or <see cref="AttributeField.Attr.ShortName"/> of.</param>
        /// <param name="fields">The <see cref="AttributeField"/>s to add to the <see cref="ArgumentDictionary"/>.</param>
        public void Add(ArgumentAttribute attr, IEnumerable<AttributeField> fields)
        {
            if (this.innerContainer.ContainsKey(attr.LongName))
            {
                this.innerContainer[attr.LongName].AddRange(fields);
            }
            else
            {
                this.innerContainer.Add(attr.LongName, fields.ToList());
            }

            if (this.innerContainer.ContainsKey(attr.ShortName))
            {
                this.innerContainer[attr.ShortName].AddRange(fields);
            }
            else
            {
                this.innerContainer.Add(attr.ShortName, fields.ToList());
            }

            this.cachedValues = null;
        }

        /// <summary>
        /// Adds the supplied <see cref="AttributeField"/> to the <see cref="ArgumentDictionary"/>.
        /// </summary>
        /// <param name="attr">The <see cref="ArgumentAttribute"/> to use the <see cref="ArgumentAttribute.LongName"/> or <see cref="AttributeField.Attr.ShortName"/> of.</param>
        /// <param name="fields">The <see cref="AttributeField"/> to add to the <see cref="ArgumentDictionary"/>.</param>
        public void Add(ArgumentAttribute attr, AttributeField field)
        {
            if (this.innerContainer.ContainsKey(attr.LongName))
            {
                this.innerContainer[attr.LongName].Add(field);
            }
            else
            {
                this.innerContainer.Add(attr.LongName, new List<AttributeField> { field });
            }

            if (this.innerContainer.ContainsKey(attr.ShortName))
            {
                this.innerContainer[attr.ShortName].Add(field);
            }
            else
            {
                this.innerContainer.Add(attr.ShortName, new List<AttributeField> { field });
            }

            this.cachedValues = null;
        }

        /// <summary>
        /// Determines whether the <see cref="ArgumentDictionary"/> contains the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="ArgumentDictionary"/>.</param>
        /// <returns>True if the <see cref="ArgumentDictionary"/> contained the key, and false otherwise.</returns>
        public bool ContainsKey(string key)
        {
            return this.innerContainer.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the <see cref="ArgumentDictionary"/> contains the specified <paramref name="argument"/>, using the <see cref="IArgument.LongName"/> and <see cref="IArgument.ShortName"/> as the indexers.
        /// </summary>
        /// <param name="argument">The key to locate in the <see cref="ArgumentDictionary"/>.</param>
        /// <returns>True if the <see cref="ArgumentDictionary"/> contained the key, and false otherwise.</returns>
        public bool ContainsKey(IArgument argument)
        {
            return this.innerContainer.ContainsKey(argument.LongName) 
                || this.innerContainer.ContainsKey(argument.ShortName);
        }
    }
}
