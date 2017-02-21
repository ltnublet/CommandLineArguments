using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments.Tests
{
    internal static class StrKnowns
    {
        public const string Default = "KnownDefault";
        public const string LongName = "KnownLongStr";
        public const string ShortName = "KS";
        public const string Example = "KnownExample";
        public const string Description = "Known Description";

        public const string PositionalOneDefault = "PositionOneDefault";
        public const string PositionalOneLongName = "PositionalLongStr";
        public const string PositionalOneShortName = "pss";
        public const string PositionalOneExample = "KnownExample1";
        public const string PositionalOneDescription = "Known Position One Description";

        public const string PositionalTwoDefault = "PositionTwoDefault";
        public const string PositionalTwoLongName = StrKnowns.PositionalOneLongName;
        public const string PositionalTwoShortName = StrKnowns.PositionalOneShortName;
        public const string PositionalTwoExample = "KnownExample2";
        public const string PositionalTwoDescription = "Known Position Two Description";

        public const string PositionalThreeDefault = "PositionThreeDefault";
        public const string PositionalThreeLongName = StrKnowns.PositionalOneLongName;
        public const string PositionalThreeShortName = StrKnowns.PositionalOneShortName;
        public const string PositionalThreeExample = "KnownExample3";
        public const string PositionalThreeDescription = "Known Position Three Description";
    }
}
