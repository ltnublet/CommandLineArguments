using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLineArguments
{
    public static class Context
    {
        public static AttributeField[] Fields
        {
            get
            {
                return fields.Value;
            }
        }
        private static Lazy<AttributeField[]> fields = new Lazy<AttributeField[]>(Context.Initialize);

        private static AttributeField[] Initialize()
        {
            Assembly.GetExecutingAssembly();
        }
    }
}
