/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using MSBuild.TeamCity.Tasks.Messages;
using Xunit;

namespace Tests
{
    public class TTeamCityMessage
    {
        private const string BuildNumber = "buildNumber";
        private const string EnableServiceMessages = "enableServiceMessages";
        private const string Number = "1.0";
        private const string Text = "t";
        private const string Path = "p";
        private const string Name = "t1";
        private const string DateRegex = @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}\+\d{2}:\d{2}";

        [Fact]
        public void BuildStat()
        {
            const string key = "k";
            var message = new BuildStatisticTeamCityMessage(key, 10);
            message.ToString().Should().Be("##teamcity[buildStatisticValue key='k' value='10.00']");
        }

        [Fact]
        public void Attributeless()
        {
            var message = new AttributeLessMessage(EnableServiceMessages);
            message.ToString().Should().Be("##teamcity[enableServiceMessages]");
        }

        [Fact]
        public void AttributelessWithTimestamp()
        {
            var message = new AttributeLessMessage(EnableServiceMessages) { IsAddTimestamp = true };
            string expected = $@"##teamcity\[enableServiceMessages timestamp='{DateRegex}'\]";
            message.ToString().Should().MatchRegex(expected);
        }

        [Fact]
        public void SimpleMessage()
        {
            var message = new SimpleTeamCityMessage(BuildNumber, Number);
            message.ToString().Should().Be("##teamcity[buildNumber '1.0']");
            message.MessageText.Should().Be(Number);
        }

        [Fact]
        public void SimpleMessageAddTimeStamp()
        {
            var message = new SimpleTeamCityMessage(BuildNumber, Number) { IsAddTimestamp = true };
            string expected = $@"##teamcity\[buildNumber '1\.0' timestamp='{DateRegex}'\]";
            message.ToString().Should().MatchRegex(expected);
        }

        [Fact]
        public void SimpleMessageFlowId()
        {
            var message = new SimpleTeamCityMessage(BuildNumber, Number)
            { IsAddTimestamp = true, FlowId = "1" };
            string expected = $@"##teamcity\[buildNumber '1\.0' timestamp='{DateRegex}' flowId='1'\]";
            message.ToString().Should().MatchRegex(expected);
        }

        [Fact]
        public void SimpleMessageFlowIdToStringTwice()
        {
            var message = new SimpleTeamCityMessage(BuildNumber, Number)
            { IsAddTimestamp = true, FlowId = "1" };
            string expected = $@"##teamcity\[buildNumber '1\.0' timestamp='{DateRegex}' flowId='1'\]";
            message.ToString().Should().MatchRegex(expected);
            message.ToString().Should().MatchRegex(expected);
        }

        [Fact]
        public void BlockOpen()
        {
            var message = new BlockOpenTeamCityMessage(Name);
            message.ToString().Should().Be("##teamcity[blockOpened name='t1']");
        }

        [Fact]
        public void BlockClose()
        {
            var message = new BlockCloseTeamCityMessage(Name);
            message.ToString().Should().Be("##teamcity[blockClosed name='t1']");
        }

        [Fact]
        public void ReportMessageFull()
        {
            const string error = "e";
            const string status = "ERROR";
            var message = new ReportMessageTeamCityMessage(Text, status, error);
            message.ToString().Should().Be("##teamcity[message text='t' status='ERROR' errorDetails='e']");
        }

        [Fact]
        public void ReportMessageStatusAndText()
        {
            const string status = "WARNING";
            var message = new ReportMessageTeamCityMessage(Text, status);
            message.ToString().Should().Be("##teamcity[message text='t' status='WARNING']");
        }

        [Fact]
        public void ReportMessageText()
        {
            var message = new ReportMessageTeamCityMessage(Text);
            message.ToString().Should().Be("##teamcity[message text='t']");
        }

        [Fact]
        public void BuildStatus()
        {
            const string status = "SUCCESS";
            var message = new BuildStatusTeamCityMessage(status, Text);
            message.ToString().Should().Be("##teamcity[buildStatus status='SUCCESS' text='t']");
        }

        [Theory]
        [InlineData("##teamcity[importData type='FxCop' path='p' parseOutOfDate='true']", true, false)]
        [InlineData("##teamcity[importData type='FxCop' path='p']", false, false)]
        [InlineData("##teamcity[importData type='FxCop' path='p' verbose='true' parseOutOfDate='true']", true, true)]
        [InlineData("##teamcity[importData type='FxCop' path='p' verbose='true']", false, true)]
        public void ImportDataContextTest(string expected, bool parseOutOfDate, bool verbose)
        {
            var context = new ImportDataContext
            {
                Type = ImportType.FxCop,
                Path = Path,
                ParseOutOfDate = parseOutOfDate,
                Verbose = verbose
            };

            var message = new ImportDataTeamCityMessage(context);
            message.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData(ImportType.FxCop, "##teamcity[importData type='FxCop' path='p']")]
        [InlineData(ImportType.Junit, "##teamcity[importData type='junit' path='p']")]
        [InlineData(ImportType.Nunit, "##teamcity[importData type='nunit' path='p']")]
        [InlineData(ImportType.Surefire, "##teamcity[importData type='surefire' path='p']")]
        [InlineData(ImportType.Pmd, "##teamcity[importData type='pmd' path='p']")]
        [InlineData(ImportType.FindBugs, "##teamcity[importData type='findBugs' path='p']")]
        [InlineData(ImportType.Mstest, "##teamcity[importData type='mstest' path='p']")]
        [InlineData(ImportType.Gtest, "##teamcity[importData type='gtest' path='p']")]
        [InlineData(ImportType.Jslint, "##teamcity[importData type='jslint' path='p']")]
        [InlineData(ImportType.CheckStyle, "##teamcity[importData type='checkstyle' path='p']")]
        [InlineData(ImportType.PmdCpd, "##teamcity[importData type='pmdCpd' path='p']")]
        [InlineData(ImportType.ReSharperDupFinder, "##teamcity[importData type='ReSharperDupFinder' path='p']")]
        public void ImportData(ImportType type, string expected)
        {
            var context = new ImportDataContext
            {
                Type = type,
                Path = Path
            };
            var message = new ImportDataTeamCityMessage(context);
            message.ToString().Should().Be(expected);
        }

        [Theory]
        [InlineData(DotNetCoverageTool.PartCover,
            "##teamcity[importData type='dotNetCoverage' path='p' tool='partcover']")]
        [InlineData(DotNetCoverageTool.Ncover, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']")]
        [InlineData(DotNetCoverageTool.Ncover3, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover3']")]
        public void ImportDataDotNetCoverage(DotNetCoverageTool tool, string expected)
        {
            var context = new ImportDataContext
            {
                Type = ImportType.DotNetCoverage,
                Path = Path
            };
            var message = new ImportDataTeamCityMessage(context, tool);
            message.ToString().Should().Be(expected);
        }

        [Fact]
        public void ImportDataDotNetCoverageNotSupported()
        {
            Assert.Throws<NotSupportedException>(delegate
            {
                var context = new ImportDataContext
                {
                    Type = ImportType.Nunit,
                    Path = Path
                };
                new ImportDataTeamCityMessage(context, DotNetCoverageTool.Ncover3);
            });
        }

        [Fact]
        public void ImportDataFindBugsNotSupported()
        {
            Assert.Throws<NotSupportedException>(delegate
            {
                var context = new ImportDataContext
                {
                    Type = ImportType.Nunit,
                    Path = Path
                };
                new ImportDataTeamCityMessage(context, "p");
            });
        }

        [Fact]
        public void ImportNullContext()
        {
            Assert.Throws<ArgumentNullException>(delegate { new ImportDataTeamCityMessage(null); });
        }

        [Fact]
        public void ImportNullContextFindBugs()
        {
            Assert.Throws<ArgumentNullException>(delegate { new ImportDataTeamCityMessage(null, "p"); });
        }

        [Fact]
        public void ImportNullContextDotNetCoverageTool()
        {
            Assert.Throws<ArgumentNullException>(
                delegate { new ImportDataTeamCityMessage(null, DotNetCoverageTool.Ncover); });
        }

        [Fact]
        public void ImportDataFindBugs()
        {
            var context = new ImportDataContext
            {
                Type = ImportType.FindBugs,
                Path = Path
            };
            var message = new ImportDataTeamCityMessage(context, "h");
            message.ToString().Should().Be("##teamcity[importData type='findBugs' path='p' findBugsHome='h']");
        }
        
        [Fact]
        public void ImportDataFindBugsException()
        {
            Assert.Throws<NotSupportedException>(
                delegate
                {
                    var context = new ImportDataContext
                    {
                        Type = ImportType.Gtest,
                        Path = Path
                    };
                    var message = new ImportDataTeamCityMessage(context, "h");
                    message.ToString().Should().Be("##teamcity[importData type='findBugs' path='p' findBugsHome='h']");
                });
        }

        [Fact]
        public void TestSuiteStart()
        {
            var message = new TestSuiteStartTeamCityMessage(Name);
            message.ToString().Should().Be("##teamcity[testSuiteStarted name='t1']");
        }

        [Fact]
        public void TestSuiteFinish()
        {
            var message = new TestSuiteFinishTeamCityMessage(Name);
            message.ToString().Should().Be("##teamcity[testSuiteFinished name='t1']");
        }

        [Fact]
        public void TestStart()
        {
            var message = new TestStartTeamCityMessage(Name, false);
            message.ToString().Should().Be("##teamcity[testStarted name='t1']");
        }

        [Fact]
        public void TestStartCaptureStandardOutput()
        {
            var message = new TestStartTeamCityMessage(Name, true);
            message.ToString().Should().Be("##teamcity[testStarted name='t1' captureStandardOutput='true']");
        }

        [Fact]
        public void TestFinish()
        {
            const double duration = 0.016;
            var message = new TestFinishTeamCityMessage(Name, duration);
            message.ToString().Should().Be("##teamcity[testFinished name='t1' duration='16']");
        }

        [Fact]
        public void TestFailed()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details);
            message.ToString().Should().Be("##teamcity[testFailed name='t1' message='m1' details='d1']");
        }

        [Fact]
        public void TestFailedFull()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = "e", Actual = "a" };
            message.ToString()
                .Should()
                .Be(
                    "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='e' actual='a']");
        }

        [Fact]
        public void TestFailedFullEmpty()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details)
            {
                Expected = string.Empty,
                Actual = string.Empty
            };
            message.ToString().Should().Be("##teamcity[testFailed name='t1' message='m1' details='d1']");
        }

        [Fact]
        public void TestFailedFullEmptyExpected()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = string.Empty, Actual = "a" };
            message.ToString()
                .Should()
                .Be("##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' actual='a']");
        }

        [Fact]
        public void TestFailedFullEmptyActual()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = "e", Actual = string.Empty };
            message.ToString()
                .Should()
                .Be("##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='e']");
        }

        [Fact]
        public void TestFailedExpectedNotSet()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details);
            message.Expected.Should().BeEmpty();
        }

        [Fact]
        public void TestFailedWithExpectedAttr()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = "e" };
            message.ToString()
                .Should()
                .Be("##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='e']");
        }

        [Fact]
        public void TestFailedWithExpectedAttrOverwrite()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = "e" };
            message.Expected = "o";
            message.ToString()
                .Should()
                .Be("##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='o']");
        }

        [Fact]
        public void TestFailedActualNotSet()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details);
            message.Actual.Should().BeEmpty();
        }

        [Fact]
        public void TestFailedWithActualAttr()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Actual = "e" };
            message.ToString()
                .Should()
                .Be("##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' actual='e']");
        }

        [Fact]
        public void TestFailedWithActualAttrOverwrite()
        {
            const string msg = "m1";
            const string details = "d1";
            var message = new TestFailedTeamCityMessage(Name, msg, details) { Actual = "e" };
            message.Actual = "o";
            message.ToString()
                .Should()
                .Be("##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' actual='o']");
        }

        [Fact]
        public void TestIgnored()
        {
            const string msg = "m1";
            var message = new TestIgnoredTeamCityMessage(Name, msg);
            message.ToString().Should().Be("##teamcity[testIgnored name='t1' message='m1']");
        }

        [Fact]
        public void TestStdOut()
        {
            const string output = "m1";
            var message = new TestStdOutTeamCityMessage(Name, output);
            message.ToString().Should().Be("##teamcity[testStdOut name='t1' out='m1']");
        }

        [Fact]
        public void TestStdErr()
        {
            const string output = "m1";
            var message = new TestStdErrTeamCityMessage(Name, output);
            message.ToString().Should().Be("##teamcity[testStdErr name='t1' out='m1']");
        }

        [Fact]
        public void DotNetCoverageInvalidKey()
        {
            Assert.Throws<ArgumentException>(delegate { new DotNetCoverMessage("i", "v"); });
        }

        [Fact]
        public void DotNetCoverageNcover3Home()
        {
            const string key = "ncover3_home";
            const string value = "v";
            var message = new DotNetCoverMessage(key, value);
            message.ToString().Should().Be("##teamcity[dotNetCoverage ncover3_home='v']");
        }

        [Theory]
        [InlineData("compilationStarted", "##teamcity[compilationStarted compiler='c']")]
        [InlineData("compilationFinished", "##teamcity[compilationFinished compiler='c']")]
        public void Compilation(string msg, string expected)
        {
            const string compiler = "c";
            var message = new CompilationMessage(compiler, msg);
            message.ToString().Should().Be(expected);
        }

        [Fact]
        public void SetParameter()
        {
            var message = new SetParameterTeamCityMessage(Name, Text);
            message.ToString().Should().Be("##teamcity[setParameter name='t1' value='t']");
        }

        [Fact]
        public void BuildProblemMandatory()
        {
            var message = new BuildProblemMessage(Text);
            message.ToString().Should().Be("##teamcity[buildProblem description='t']");
        }

        [Fact]
        public void BuildProblemAll()
        {
            var message = new BuildProblemMessage(Text, Name);
            message.ToString().Should().Be("##teamcity[buildProblem description='t' identity='t1']");
        }
    }
}