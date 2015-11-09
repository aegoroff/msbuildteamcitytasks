/*
 * Created by: egr
 * Created at: 28.08.2010
 * © 2007-2015 Alexander Egorov
 */

using FluentAssertions;
using MSBuild.TeamCity.Tasks.Internal;
using Xunit;

namespace Tests
{
    public class TGoogleTestArgumentsBuilder
    {
        private const string OutputXml = "--gtest_output=xml:";
        private const string DisableTestsCommand = "--gtest_also_run_disabled_tests";
        private const string CatchExceptionsCommand = "--gtest_catch_exceptions";
        private const string Space = " ";
        private const string FilterCommand = "--gtest_filter=1";
        private const string Filter = "1";
        private const string Empty = "";

        [Theory]
        [InlineData(false, false, Empty, OutputXml)]
        [InlineData(false, false, null, OutputXml)]
        [InlineData(true, false, Empty, OutputXml + Space + CatchExceptionsCommand)]
        [InlineData(false, true, Empty, OutputXml + Space + DisableTestsCommand)]
        [InlineData(false, false, Filter, OutputXml + Space + FilterCommand)]
        [InlineData(true, true, Empty, OutputXml + Space + DisableTestsCommand + Space + CatchExceptionsCommand)]
        [InlineData(false, true, Filter, OutputXml + Space + DisableTestsCommand + Space + FilterCommand)]
        [InlineData(true, false, Filter, OutputXml + Space + CatchExceptionsCommand + Space + FilterCommand)]
        [InlineData(true, true, Filter,
            OutputXml + Space + DisableTestsCommand + Space + CatchExceptionsCommand + Space + FilterCommand)]
        public void CreateCommandLine(bool catchExceptions, bool runDisabledTests, string filter, string expected)
        {
            var builder = new GoogleTestCommandLine(catchExceptions, runDisabledTests, filter);
            builder.ToString().Should().Be(expected);
        }
    }
}