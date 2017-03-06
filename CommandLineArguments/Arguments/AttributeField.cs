using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Arguments
{
    /// <summary>
    /// Essentially a named tuple for pairs of <see cref="ArgumentAttribute"/>s and <see cref="FieldInfo"/>s.
    /// </summary>
    internal class AttributeField
    {
        /// <summary>
        /// Instantiates an <see cref="AttributeField"/> object using the supplied parameters.
        /// </summary>
        /// <param name="attribute">The <see cref="ArgumentAttribute"/> of the <see cref="AttributeField"/>.</param>
        /// <param name="field">The <see cref="FieldInfo"/> of the <see cref="AttributeField"/>.</param>
        public AttributeField(ArgumentAttribute attribute, FieldInfo field)
        {
            this.Attr = attribute;
            this.Field = field;
        }

        /// <summary>
        /// The <see cref="ArgumentAttribute"/>.
        /// </summary>
        public ArgumentAttribute Attr { get; private set; }

        /// <summary>
        /// The <see cref="FieldInfo"/>.
        /// </summary>
        public FieldInfo Field { get; private set; }
    }
}
