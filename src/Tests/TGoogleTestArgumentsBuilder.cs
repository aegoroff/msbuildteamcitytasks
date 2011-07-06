/*
 * Created by: egr
 * Created at: 28.08.2010
 * © 2007-2011 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TGoogleTestArgumentsBuilder
    {
        private const string OutputXml = "--gtest_output=xml:";
        private const string DisableTestsCommand = "--gtest_also_run_disabled_tests";
        private const string CatchExceptionsCommand = "--gtest_catch_exceptions";
        private const string Space = " ";
        private const string FilterCommand = "--gtest_filter=1";
        private const string Filter = "1";
        private const string Empty = "";

        [TestCase(false, false, Empty, OutputXml)]
        [TestCase(false, false, null, OutputXml)]
        [TestCase(true, false, Empty, OutputXml + Space + CatchExceptionsCommand)]
        [TestCase(false, true, Empty, OutputXml + Space + DisableTestsCommand)]
        [TestCase(false, false, Filter, OutputXml + Space + FilterCommand)]
        [TestCase(true, true, Empty, OutputXml + Space + DisableTestsCommand + Space + CatchExceptionsCommand)]
        [TestCase(false, true, Filter, OutputXml + Space + DisableTestsCommand + Space + FilterCommand)]
        [TestCase(true, false, Filter, OutputXml + Space + CatchExceptionsCommand + Space + FilterCommand)]
        [TestCase(true, true, Filter,
            OutputXml + Space + DisableTestsCommand + Space + CatchExceptionsCommand + Space + FilterCommand)]
        public void CreateCommandLine(bool catchExceptions, bool runDisabledTests, string filter, string expected)
        {
            GoogleTestCommandLine builder = new GoogleTestCommandLine(catchExceptions, runDisabledTests, filter);
            Assert.That(builder.ToString(), Is.EqualTo(expected));
        }
    }
}