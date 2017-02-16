using System;
using Xunit;

namespace Arguments.Tests
{
    public class ContextTests
    {
        [ArgumentAttribute(StrKnowns.Default, StrKnowns.LongName, StrKnowns.ShortName, StrKnowns.Example, StrKnowns.Description)]
        public string StrAttribute;

        [ArgumentAttribute(DblKnowns.Default, DblKnowns.LongName, DblKnowns.ShortName, DblKnowns.Example, DblKnowns.Description)]
        public double DblAttribute;

        [ArgumentAttribute(IntKnowns.Default, IntKnowns.LongName, IntKnowns.ShortName, IntKnowns.Example, IntKnowns.Description)]
        public int IntAttribute;

        [Fact]
        public void Initialize_RegisteredInstances_ShouldSucceed()
        {
            ContextTests instanceOne = new ContextTests();
            ContextTests instanceTwo = new ContextTests();

            Context.Register(this);
            Context.Register(instanceOne);
            Context.Register(instanceTwo);

            Context.Initialize();

            Assert.True(ContextTests.FieldsEqual(this));
            Assert.True(ContextTests.FieldsEqual(instanceOne));
            Assert.True(ContextTests.FieldsEqual(instanceTwo));
        }

        [Fact]
        public void Initialize_SpecificInstance_ShouldSucceed()
        {
            Context.Initialize(this);

            Assert.True(ContextTests.FieldsEqual(this));
        }

        private static bool FieldsEqual(ContextTests instance)
        {
            return StrKnowns.Default.Equals(instance.StrAttribute)
                && DblKnowns.Default.Equals(instance.DblAttribute.ToString())
                && IntKnowns.Default.Equals(instance.IntAttribute.ToString());
        }
    }
}
