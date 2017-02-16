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
        private static BindingFlags bindingFlags = 
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField;

        private static Lazy<AttributeField[]> fields = new Lazy<AttributeField[]>(Context.Iterate);

        private static List<object> instances = new List<object>();

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
        /// Parse <paramref name="args"/> using the <paramref name="parameterDelimiters"/>, and set all instance fields to either their default or supplied value.
        /// </summary>
        /// <param name="args">The arguments to use as the source of user-supplied values. When null, initializes using all defaults.</param>
        /// <param name="parameterDelimiters">The allowed parameter delimiters preceeding long or short names.</param>
        /// <param name="helpParameter">The help parameter - if present in <paramref name="args"/>, short-circuit setting instance field values.</param>
        /// <returns>True if help was requested, and false otherwise.</returns>
        public static bool Initialize(string[] args = null, string[] parameterDelimiters = null, string helpParameter = null)
        {
            if (args == null)
            {
                args = new string[] { };
            }

            if (parameterDelimiters == null)
            {
                parameterDelimiters = new string[] { };
            }

            if (helpParameter == null)
            {
                helpParameter = string.Empty;
            }

            if (parameterDelimiters.Select(
                delimiter => 
                    delimiter + helpParameter)
                .Any(helpRequested => 
                    args.Any(argument => 
                        argument.Equals(helpRequested))))
            {
                return true;
            }
            else
            {
                List<AttributeField> userSupplied = new List<AttributeField>();

                for (int arg = 0; arg < args.Length - 2; arg += 2)
                {
                    throw new NotImplementedException();
                }

                Context.SetInstanceFieldValues(userSupplied.ToArray());
            }

            return false;
        }

        /// <summary>
        /// Register an object instance as having a field which receives its value from an <see cref="ArgumentAttribute"/>.
        /// </summary>
        /// <param name="instance"></param>
        public static void Register(object instance)
        {
            Context.instances.Add(instance);
        }

        /// <summary>
        /// Reflects over the assembly to populate the <see cref="Fields"/>.
        /// </summary>
        /// <returns>
        /// An array of <see cref="AttributeField"/>s representing all fields in the executing assembly decorated with the <see cref="ArgumentAttribute"/>.
        /// </returns>
        private static AttributeField[] Iterate()
        {
            List<AttributeField> scratch = new List<AttributeField>();
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    types.AddRange(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException)
                {
                    // Ignore this exception type.
                }
            }
            
            foreach (Type type in types)
            {
                foreach (FieldInfo field in type.GetFields(Context.bindingFlags))
                {
                    ArgumentAttribute attribute = field.GetCustomAttributes<ArgumentAttribute>().FirstOrDefault();
                    if (attribute != null)
                    {
                        scratch.Add(new AttributeField(attribute, field));
                    }
                }
            }

            return scratch.ToArray();
        }

        /// <summary>
        /// For all registered <see cref="Fields"/> except those in <paramref name="ignore"/>, set the associated instance's field to the default.
        /// </summary>
        /// <param name="ignore">The <see cref="AttributeField"/>s to ignore - these will not have their assigned value changed.</param>
        private static void SetInstanceFieldValues(AttributeField[] ignore)
        {
            foreach (AttributeField field in Context.Fields.Except(ignore))
            {
                field.Field.SetValue(
                    Context.instances.Single(
                        x =>
                        x.GetType()
                            .GetFields(Context.bindingFlags)
                            .Contains(field.Field)),
                    Convert.ChangeType(field.Attr.DefaultValue, field.Field.FieldType));
            }
        }
    }
}
