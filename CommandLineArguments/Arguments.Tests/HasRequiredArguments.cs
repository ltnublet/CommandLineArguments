using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.Arguments;

namespace Arguments.Tests
{
    public class HasRequiredArguments
    {
        [Argument("defaultValue", "longName", "s", "exampleValue", "description", 0, true)]
        public string Arg;

        [Argument("defaultValue", "longName2", "s2", "exampleValue", "description", 0, true)]
        public string Arg2;
    }
}
