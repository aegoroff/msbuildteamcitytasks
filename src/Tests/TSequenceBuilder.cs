/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using FluentAssertions;
using MSBuild.TeamCity.Tasks.Internal;
using Xunit;

namespace Tests
{
    public class TSequenceBuilder
    {
        private const string Filter = "(1,2)";
        private const string Head = "(";
        private const string Trail = ")";
        private const string Separator = ",";
        private readonly int[] values = { 1, 2 };

        [Theory]
        [InlineData(Filter, new[] { 1, 2 }, Head, Trail)]
        [InlineData("", new int[0], Head, Trail)]
        [InlineData("1,2", new[] { 1, 2 }, null, null)]
        public void OneCall(string expected, IEnumerable<int> enumerator, string head, string trail)
        {
            var builder = new SequenceBuilder<int>(enumerator, Separator, head, trail);
            builder.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData(Filter, new[] { 1, 2 })]
        [InlineData("", new int[0])]
        public void ManyCalls(string expected, IEnumerable<int> enumerator)
        {
            var builder = new SequenceBuilder<int>(enumerator, Separator, Head, Trail);
            builder.ToString().Should().Be(expected);
            builder.ToString().Should().Be(expected);
        }

        [Fact]
        public void SimplifiedConstructor()
        {
            var builder = new SequenceBuilder<int>(this.values, Separator);
            builder.ToString().Should().Be("1,2");
        }
    }
}