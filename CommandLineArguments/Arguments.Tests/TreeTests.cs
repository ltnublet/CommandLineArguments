using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Arguments.Collections;

namespace Arguments.Tests
{
    public class TreeTests
    {
        /// <summary>
        /// This test could probably be refactored so that failure messages make more sense; right now, it's a giant
        /// integration of most of the Arguments.Collections namespace.
        /// </summary>
        [Fact]
        public void Constructor_GoodValues_ShouldSucceed()
        {
            TreeNode<string> root = 
                new TreeNode<string>("Root", null, new List<TreeNode<string>>
                {
                    new TreeNode<string>("ChildOne", null, new List<TreeNode<string>>
                    {
                        new TreeNode<string>("SubChildOneOne"),
                        new TreeNode<string>("SubChildOneTwo")
                    }),
                    new TreeNode<string>("ChildTwo", null, new List<TreeNode<string>>
                    {
                        new TreeNode<string>("SubChildTwoOne", null, new List<TreeNode<string>>
                        {
                            new TreeNode<string>("SubSubChildTwoOneOne")
                        })
                    })
                });

            Tree<string> test = new Tree<string>(root);

            StringBuilder builder = new StringBuilder();
            foreach (string value in test)
            {
                builder.AppendLine(value);
            }

            Assert.Equal(@"Root
ChildOne
SubChildOneOne
SubChildOneTwo
ChildTwo
SubChildTwoOne
SubSubChildTwoOneOne
",              builder.ToString());
        }
    }
}
