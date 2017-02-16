using System;
using Xunit;

namespace CommandLineArguments.Tests
{
    public class ArgumentAttributesTests
    {
        [ArgumentAttribute(StrKnowns.Default, StrKnowns.LongName, StrKnowns.ShortName, StrKnowns.Example, StrKnowns.Description)]
        public string StrAttribute;

        [ArgumentAttribute(DblKnowns.Default, DblKnowns.LongName, DblKnowns.ShortName, DblKnowns.Example, DblKnowns.Description)]
        public double DblAttribute;

        [ArgumentAttribute(IntKnowns.Default, IntKnowns.LongName, IntKnowns.ShortName, IntKnowns.Example, IntKnowns.Description)]
        public int IntAttribute;

        [Fact]
        public void SetDefaults_KnownValues_ShouldSucceed()
        {

        }
    }
}
