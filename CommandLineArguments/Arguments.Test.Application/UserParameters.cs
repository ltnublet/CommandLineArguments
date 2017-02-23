using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arguments.Test.Application
{
    public class UserParameters
    {
        [Argument("1", "StatusCode", "s", "int32.MinValue:int32.MaxValue", "Status code to return with")]
        public int StatusCode;
        [Argument(null, "Username", "u", @"DOMAIN\username", "The username to use during execution")]
        public string Username;
        [Argument("False", "Execute", "e", "True", "Indicated whether execution should occur", -1)]
        public bool Execute;

        public UserParameters()
        {
            Context.Register(this);

            // TODO: see if it's possible to make it that, upon accessing a field decorated with ArgumentAttribute,
            // initialization is triggered for the instance.
        }
    }
}
