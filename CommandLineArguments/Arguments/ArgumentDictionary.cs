using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arguments
{
    /// <summary>
    /// Represents a collection of <see cref="AttributeField"/>s grouped by <see cref="AttributeField.Attr.LongName"/>.
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
                    this.cachedValues = this.innerContainer.Values.Select(x =>
                        x.AsEnumerable()).Aggregate((y, z) => z.Concat(y));
                }

                return this.cachedValues;
            }
        }

        /// <summary>
        /// Gets the group of <see cref="AttributeField"/>s with the corresponding <see cref="ArgumentAttribute.LongName"/>.
        /// </summary>
        /// <param name="key">The <see cref="ArgumentAttribute.LongName"/> to index on.</param>
        /// <returns>The corresponding group of <see cref="AttributeField"/>s.</returns>
        public IEnumerable<AttributeField> this[string key]
        {
            get
            {
                return this.innerContainer[key];
            }
        }

        /// <summary>
        /// Adds the supplied <see cref="AttributeField"/>s to the <see cref="ArgumentDictionary"/>.
        /// </summary>
        /// <param name="attr">The <see cref="ArgumentAttribute"/> to use the <see cref="ArgumentAttribute.LongName"/> of.</param>
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

            this.cachedValues = null;
        }

        /// <summary>
        /// Adds the supplied <see cref="AttributeField"/> to the <see cref="ArgumentDictionary"/>.
        /// </summary>
        /// <param name="attr">The <see cref="ArgumentAttribute"/> to use the <see cref="ArgumentAttribute.LongName"/> of.</param>
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

            this.cachedValues = null;
        }
    }
}
