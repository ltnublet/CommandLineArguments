using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Arguments.Collections;

namespace Arguments
{
    /// <summary>
    /// Maintains the executing context.
    /// </summary>
    public static class Context
    {
        private static BindingFlags bindingFlags = 
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField;

        private static Lazy<ArgumentDictionary> fields = new Lazy<ArgumentDictionary>(Context.Iterate);

        /// <summary>
        /// Backing field for the <see cref="Context.instances"/> property. Used to allow the garbage collector to
        /// clean up objects without the user needing to keep track of what they've registered.
        /// </summary>
        private static List<WeakReference<object>> backingInstances = new List<WeakReference<object>>();

        /// <summary>
        /// All fields decorated with <see cref="ArgumentAttribute"/>s in the executing assembly.
        /// </summary>
        public static IEnumerable<AttributeField> Fields
        {
            get
            {
                return fields.Value.Values;
            }
        }

        /// <summary>
        /// Hides the <see cref="Context.backingInstances"/> field from consumers to simplify use by removing invalid
        /// objects before returning them.
        /// </summary>
        private static IEnumerable<object> instances
        {
            get
            {
                backingInstances.RemoveAll(x => !x.TryGetTarget(out object buffer));
                return backingInstances.Select(x =>
                {
                    x.TryGetTarget(out object buffer);
                    return buffer;
                });
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

                Tree<string> parsed = Context.ParseArgs("Context", parameterDelimiters, args);
                foreach (TreeNode<string> argument in parsed.Root.Children)
                {
                    if (fields.Value.ContainsKey(argument.Value))
                    {
                        IEnumerable<AttributeField> currentPosition = fields.Value[argument.Value];
                        if (currentPosition.Count() == 1                    // There is a single expected field for that attribute.
                            && currentPosition.First().Attr.Position == -1  // The only expected field is a flag.
                            && argument.Children.Count == 0)                // No value was supplied for the field.
                        {
                            Context.SetInstanceFieldValue(
                                "True", 
                                currentPosition.First(), 
                                Context.instances);

                            userSupplied.Add(currentPosition.First());
                        }
                        else if (currentPosition.Count() == argument.Children.Count)
                        {
                            for (int counter = 0; counter < argument.Children.Count; counter++)
                            {
                                AttributeField currentField = currentPosition.ElementAt(counter);
                                Context.SetInstanceFieldValue(
                                    argument.Children[counter].Value,
                                    currentField,
                                    Context.instances);

                                userSupplied.Add(currentField);
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Malformed argument \"{argument.Value}\".");
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Unrecognized argument \"{argument.Value}\".");
                    }
                }

                Context.SetInstanceFieldValues(Context.Fields.Except(userSupplied), Context.instances);
            }

            return false;
        }

        /// <summary>
        /// Set all instance fields to their default values for only a single instance.
        /// </summary>
        /// <param name="instance">The object to perform the operation on.</param>
        public static void Initialize(object instance)
        {
            Context.SetInstanceFieldValues(Context.Fields, new object[] { instance });
        }

        /// <summary>
        /// Register an object instance as having a field which receives its value from an <see cref="ArgumentAttribute"/>.
        /// </summary>
        /// <param name="instance"></param>
        public static void Register(object instance)
        {
            if (instance != null)
            {
                Context.backingInstances.Add(new WeakReference<object>(instance));
            }
        }

        /// <summary>
        /// Converts the supplied <paramref name="args"/> to a <see cref="Tree{string}"/>, using the <paramref name="parameterDelimiters"/> as the set of valid parameter delimiters upon which to split it.
        /// </summary>
        /// <param name="rootValue">The value of the root element.</param>
        /// <param name="parameterDelimiters">The set of valid parameter delimiters.</param>
        /// <param name="args">The arguments to convert to a <see cref="Tree{string}"/>.</param>
        /// <returns>A <see cref="Tree{string}"/> representing the supplied <paramref name="args"/>, with a root of value <paramref name="rootValue"/>.</returns>
        internal static Tree<string> ParseArgs(string rootValue, IEnumerable<string> parameterDelimiters, string[] args)
        {
            TreeNode<string> root = new TreeNode<string>(rootValue);
            TreeNode<string> current = root;
            for (int arg = 0; arg < args.Length; arg++)
            {
                string debug = args[arg];
                TreeNode<string> newNode = new TreeNode<string>(StringUtil.Chop(args[arg], parameterDelimiters));

                if (newNode.Value != args[arg])
                {
                    current = newNode;
                    root.Add(newNode);
                }
                else
                {
                    current.Add(newNode);
                }
            }

            return new Tree<string>(root);
        }

        /// <summary>
        /// Reflects over the assembly to populate the <see cref="Fields"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="ArgumentDictionary"/>s representing all fields in the executing assembly decorated with an <see cref="ArgumentAttribute"/>.
        /// </returns>
        private static ArgumentDictionary Iterate()
        {
            ArgumentDictionary returnValue = new ArgumentDictionary();
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
                        returnValue.Add(attribute, new AttributeField(attribute, field));
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// For all specified <see cref="AttributeField"/>s, for all instances in the <paramref name="instanceSource"/>, set the associated fields to the default.
        /// </summary>
        /// <param name="include">The <see cref="AttributeField"/>s to include - these will have their assigned value changed.</param>
        /// <param name="instanceSource">The instances upon which to change the associated field values.</param>
        private static void SetInstanceFieldValues(
            IEnumerable<AttributeField> include, 
            IEnumerable<object> instanceSource)
        {
            foreach (AttributeField field in include)
            {
                Context.SetInstanceFieldValue(field.Attr.DefaultValue, field, instanceSource);
            }
        }

        /// <summary>
        /// For the specified <see cref="AttributeField"/> <paramref name="field"/>, for all instances in the <paramref name="instanceSource"/>, set the associated field to the supplied <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to use for the <paramref name="field"/>'s new value.</param>
        /// <param name="field">The <see cref="AttributeField"/> to target - instances in <paramref name="instanceSource"/> will have their value changed for this field.</param>
        /// <param name="instanceSource">The instances upon which to change the associated fields values.</param>
        private static void SetInstanceFieldValue(
            string value,
            AttributeField field,
            IEnumerable<object> instanceSource)
        {
            foreach (object instance in
                instanceSource.Where(x => x.GetType().GetFields(Context.bindingFlags).Contains(field.Field)))
            {
                field.Field.SetValue(instance, Convert.ChangeType(value, field.Field.FieldType));
            }
        }
    }
}
