using System.Linq;
using Xunit;
using Drexel.Arguments;
using Drexel.Arguments.Collections;
using System;
using System.Reflection;

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

        [ArgumentAttribute(StrKnowns.PositionalTwoDefault, StrKnowns.PositionalTwoLongName, StrKnowns.PositionalTwoShortName, StrKnowns.PositionalTwoExample, StrKnowns.PositionalTwoDescription, 1)]
        public string StrAttributePositionalTwo;

        [ArgumentAttribute(StrKnowns.PositionalOneDefault, StrKnowns.PositionalOneLongName, StrKnowns.PositionalOneShortName, StrKnowns.PositionalOneExample, StrKnowns.PositionalOneDescription, 0)]
        public string StrAttributePositionalOne;

        [ArgumentAttribute(StrKnowns.PositionalThreeDefault, StrKnowns.PositionalThreeLongName, StrKnowns.PositionalThreeShortName, StrKnowns.PositionalThreeExample, StrKnowns.PositionalThreeDescription, 2)]
        public string StrAttributePositionalThree;

        [ArgumentAttribute(BoolKnowns.Default, BoolKnowns.LongName, BoolKnowns.ShortName, BoolKnowns.Example, BoolKnowns.Description, -1)]
        public bool BoolAttributeFlag;

        // HACK: Because Context is static, we reset the instance before each test.
        public ContextTests()
        {
            ContextTests.PerformReset();
        }

        [Fact]
        public void Initialize_RegisteredInstances_ShouldSucceed()
        {
            ContextTests instanceOne = new ContextTests();
            ContextTests instanceTwo = new ContextTests();

            Context.Register(this);
            Context.Register(instanceOne);
            Context.Register(instanceTwo);

            Context.Initialize();
            Context.Invoke();

            Assert.True(ContextTests.DefaultFieldsEqual(this));
            Assert.True(ContextTests.DefaultFieldsEqual(instanceOne));
            Assert.True(ContextTests.DefaultFieldsEqual(instanceTwo));
        }

        [Fact]
        public void Initialize_SpecificInstance_ShouldSucceed()
        {
            Context.Initialize(this);

            Assert.True(ContextTests.DefaultFieldsEqual(this));
        }

        [Fact]
        public void Initialize_RegisteredInstances_NonDefaultValues_ShouldSucceed()
        {
            const string testValue = "This Is The Big Test";

            Context.Register(this);

            Context.Initialize(new string[] { $"-{StrKnowns.LongName}", testValue }, new string[] { "-" });

            Context.Invoke();

            Assert.Equal(testValue, this.StrAttribute);
            Assert.Equal(double.Parse(DblKnowns.Default), this.DblAttribute);
            Assert.Equal(int.Parse(IntKnowns.Default), this.IntAttribute);
        }

        [Fact]
        public void Initialize_RegisteredInstances_MultiPositionalArguments_ShouldSucceed()
        {
            const string testValue1 = "POSITION_ONE";
            const string testValue2 = "POSITION_TWO";
            const string testValue3 = "POSITION_THREE";

            Context.Register(this);

            Context.Initialize(
                new string[] { $"-{StrKnowns.PositionalOneLongName}", testValue1, testValue2, testValue3 },
                new string[] { "-" });

            Context.Invoke();

            Assert.Equal(testValue1, this.StrAttributePositionalOne);
            Assert.Equal(testValue2, this.StrAttributePositionalTwo);
            Assert.Equal(testValue3, this.StrAttributePositionalThree);
        }

        [Fact]
        public void Initialize_RegisteredInstances_FlagArguments_ShouldSucceed()
        {
            Context.Register(this);

            Context.Initialize(new string[] { $"-{BoolKnowns.LongName}" }, new string[] { "-" });

            Context.Invoke();

            Assert.Equal(true, this.BoolAttributeFlag);
        }

        [Fact]
        public void ParseArgs_SuppliedArgs_ShouldSucceed()
        {
            Tree<string> result = Context.ParseArgs(
                "Context",
                new string[] { "-" }.AsEnumerable(),
                new string[] {
                    "-this", "is", "a", "test",
                    "-in", "which",
                    "-I",
                    "-supply",
                    "-many", "arguments", "with",
                    "-a", "valid",
                    "-delimiter" });

            // TODO: Should probably expose a better way of indexing into the tree than this.
            Assert.Equal("this", result.Root.Children.First().Value);
            Assert.Equal("in", result.Root.Children.Skip(1).First().Value);
            Assert.Equal("I", result.Root.Children.Skip(2).First().Value);
            Assert.Equal("supply", result.Root.Children.Skip(3).First().Value);
            Assert.Equal("many", result.Root.Children.Skip(4).First().Value);
            Assert.Equal("a", result.Root.Children.Skip(5).First().Value);
            Assert.Equal("delimiter", result.Root.Children.Skip(6).First().Value);

            Assert.Equal("is", result.Root.Children.First().Children.First().Value);
            Assert.Equal("a", result.Root.Children.First().Children.Skip(1).First().Value);
            Assert.Equal("test", result.Root.Children.First().Children.Skip(2).First().Value);

        }

        private static bool DefaultFieldsEqual(ContextTests instance)
        {
            return StrKnowns.Default.Equals(instance.StrAttribute)
                && DblKnowns.Default.Equals(instance.DblAttribute.ToString())
                && IntKnowns.Default.Equals(instance.IntAttribute.ToString());
        }

        private static void PerformReset()
        {
            Type staticType = typeof(Context);
            ConstructorInfo ci = staticType.TypeInitializer;
            object[] parameters = new object[0];
            ci.Invoke(null, parameters);
        }
    }
}
