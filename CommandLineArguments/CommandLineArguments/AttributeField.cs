using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommandLineArguments
{
    public class AttributeField
    {
        public AttributeField(ArgumentAttribute attribute, FieldInfo field)
        {
            this.Attr = attribute;
            this.Field = field;
        }

        public ArgumentAttribute Attr { get; private set; }

        public FieldInfo Field { get; private set; }
    }
}
