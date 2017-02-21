using Arguments.Collections;
using System;
using System.Linq;
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

            Context.Initialize(new string[] { "-KnownLongStr", testValue }, new string[] { "-" });

            Assert.Equal(testValue, this.StrAttribute);
            Assert.Equal(double.Parse(DblKnowns.Default), this.DblAttribute);
            Assert.Equal(int.Parse(IntKnowns.Default), this.IntAttribute);
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
    }
}
