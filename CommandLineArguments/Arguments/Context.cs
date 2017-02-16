using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Arguments
{
    /// <summary>
    /// Maintains the executing context.
    /// </summary>
    public static class Context
    {
        private static Lazy<AttributeField[]> fields = new Lazy<AttributeField[]>(Context.Initialize);
        
        /// <summary>
        /// All fields decorated with <see cref="ArgumentAttribute"/>s in the executing assembly.
        /// </summary>
        public static AttributeField[] Fields
        {
            get
            {
                return fields.Value;
            }
        }

        /// <summary>
        /// Reflects over the assembly to populate the <see cref="Fields"/>.
        /// </summary>
        /// <returns>
        /// An array of <see cref="AttributeField"/>s representing all fields in the executing assembly decorated with the <see cref="ArgumentAttribute"/>.
        /// </returns>
        private static AttributeField[] Initialize()
        {
            List<AttributeField> scratch = new List<AttributeField>();

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (FieldInfo field in type.GetFields(
                    BindingFlags.Public 
                    | BindingFlags.NonPublic 
                    | BindingFlags.Instance 
                    | BindingFlags.GetField))
                {
                    ArgumentAttribute attribute = type.GetCustomAttributes<ArgumentAttribute>().FirstOrDefault();
                    if (attribute != null)
                    {
                        scratch.Add(new AttributeField(attribute, field));
                    }
                }
            }

            return scratch.ToArray();
        }
    }
}
