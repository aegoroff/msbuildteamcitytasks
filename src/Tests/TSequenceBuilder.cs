/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2012 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TSequenceBuilder
    {
        private const string Filter = "(1,2)";
        private const string Head = "(";
        private const string Trail = ")";
        private const string Separator = ",";
        private readonly int[] values = new[] { 1, 2 };

        [TestCase(Filter, new[] { 1, 2 }, Head, Trail)]
        [TestCase("", new int[0], Head, Trail)]
        [TestCase("1,2", new[] { 1, 2 }, null, null)]
        public void OneCall(string expected, IEnumerable<int> enumerator, string head, string trail)
        {
            SequenceBuilder<int> builder = new SequenceBuilder<int>(enumerator, Separator, head, trail);
            Assert.That(builder.ToString(), Is.EqualTo(expected));
        }

        [TestCase(Filter, new[] { 1, 2 })]
        [TestCase("", new int[0])]
        public void ManyCalls(string expected, IEnumerable<int> enumerator)
        {
            SequenceBuilder<int> builder = new SequenceBuilder<int>(enumerator, Separator, Head, Trail);
            Assert.That(builder.ToString(), Is.EqualTo(expected));
            Assert.That(builder.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void SimplifiedConstructor()
        {
            SequenceBuilder<int> builder = new SequenceBuilder<int>(values, Separator);
            Assert.That(builder.ToString(), Is.EqualTo("1,2"));
        }
    }
}