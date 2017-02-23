using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arguments.Tests
{
    public class HeapWalkerTests
    {
        [Fact]
        public void GetHeapObjects_Call_ShouldSucceed()
        {
            HeapWalker.GetHeapObjects(1000);
        }
    }
}
