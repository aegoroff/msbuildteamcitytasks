/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2012 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks.Messages;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TTeamCityMessage
    {
        private const string BuildNumber = "buildNumber";
        private const string EnableServiceMessages = "enableServiceMessages";
        private const string Number = "1.0";
        private const string Text = "t";
        private const string Path = "p";
        private const string Name = "t1";
        private const string DateRegex = @"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d{3}\+\d{2}:\d{2}";

        [Test]
        public void BuildStat()
        {
            const string key = "k";
            BuildStatisticTeamCityMessage message = new BuildStatisticTeamCityMessage(key, 10);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildStatisticValue key='k' value='10.00']"));
        }

        [Test]
        public void Attributeless()
        {
            AttributeLessMessage message = new AttributeLessMessage(EnableServiceMessages);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[enableServiceMessages]"));
        }

        [Test]
        public void AttributelessWithTimestamp()
        {
            AttributeLessMessage message = new AttributeLessMessage(EnableServiceMessages) { IsAddTimestamp = true };
            string expected = string.Format(@"##teamcity\[enableServiceMessages timestamp='{0}'\]", DateRegex);
            Assert.That(message.ToString(), Is.StringMatching(expected));
        }

        [Test]
        public void SimpleMessage()
        {
            SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, Number);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0']"));
            Assert.That(message.MessageText, Is.EqualTo(Number));
        }

        [Test]
        public void SimpleMessageAddTimeStamp()
        {
            SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, Number) { IsAddTimestamp = true };
            string expected = string.Format(@"##teamcity\[buildNumber '1\.0' timestamp='{0}'\]", DateRegex);
            Assert.That(message.ToString(), Is.StringMatching(expected));
        }

        [Test]
        public void SimpleMessageFlowId()
        {
            SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, Number)
                                                { IsAddTimestamp = true, FlowId = "1" };
            string expected = string.Format(@"##teamcity\[buildNumber '1\.0' timestamp='{0}' flowId='1'\]", DateRegex);
            Assert.That(message.ToString(), Is.StringMatching(expected));
        }

        [Test]
        public void SimpleMessageFlowIdToStringTwice()
        {
            SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, Number)
                                                { IsAddTimestamp = true, FlowId = "1" };
            string expected = string.Format(@"##teamcity\[buildNumber '1\.0' timestamp='{0}' flowId='1'\]", DateRegex);
            Assert.That(message.ToString(), Is.StringMatching(expected));
            Assert.That(message.ToString(), Is.StringMatching(expected));
        }

        [Test]
        public void BlockOpen()
        {
            BlockOpenTeamCityMessage message = new BlockOpenTeamCityMessage(Name);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[blockOpened name='t1']"));
        }

        [Test]
        public void BlockClose()
        {
            BlockCloseTeamCityMessage message = new BlockCloseTeamCityMessage(Name);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[blockClosed name='t1']"));
        }

        [Test]
        public void ReportMessageFull()
        {
            const string error = "e";
            const string status = "ERROR";
            ReportMessageTeamCityMessage message = new ReportMessageTeamCityMessage(Text, status, error);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[message text='t' status='ERROR' errorDetails='e']"));
        }

        [Test]
        public void ReportMessageStatusAndText()
        {
            const string status = "WARNING";
            ReportMessageTeamCityMessage message = new ReportMessageTeamCityMessage(Text, status);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[message text='t' status='WARNING']"));
        }

        [Test]
        public void ReportMessageText()
        {
            ReportMessageTeamCityMessage message = new ReportMessageTeamCityMessage(Text);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[message text='t']"));
        }

        [Test]
        public void BuildStatus()
        {
            const string status = "SUCCESS";
            BuildStatusTeamCityMessage message = new BuildStatusTeamCityMessage(status, Text);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildStatus status='SUCCESS' text='t']"));
        }

        [Test]
        public void ImportData()
        {
            const string type = "FxCop";
            ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(type, Path);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[importData type='FxCop' path='p']"));
        }

        [TestCase(ImportType.FxCop, "##teamcity[importData type='FxCop' path='p']")]
        [TestCase(ImportType.Junit, "##teamcity[importData type='junit' path='p']")]
        [TestCase(ImportType.Nunit, "##teamcity[importData type='nunit' path='p']")]
        [TestCase(ImportType.Surefire, "##teamcity[importData type='surefire' path='p']")]
        [TestCase(ImportType.Pmd, "##teamcity[importData type='pmd' path='p']")]
        [TestCase(ImportType.FindBugs, "##teamcity[importData type='findBugs' path='p']")]
        [TestCase(ImportType.Mstest, "##teamcity[importData type='mstest' path='p']")]
        [TestCase(ImportType.Gtest, "##teamcity[importData type='gtest' path='p']")]
        [TestCase(ImportType.Jslint, "##teamcity[importData type='jslint' path='p']")]
        [TestCase(ImportType.CheckStyle, "##teamcity[importData type='checkstyle' path='p']")]
        [TestCase(ImportType.PmdCpd, "##teamcity[importData type='pmdCpd' path='p']")]
        [TestCase(ImportType.ReSharperDupFinder, "##teamcity[importData type='ReSharperDupFinder' path='p']")]
        public void ImportData(ImportType type, string expected)
        {
            ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(type, Path);
            Assert.That(message.ToString(), Is.EqualTo(expected));
        }

        [TestCase(DotNetCoverageTool.PartCover,
            "##teamcity[importData type='dotNetCoverage' path='p' tool='partcover']")]
        [TestCase(DotNetCoverageTool.Ncover, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']")]
        [TestCase(DotNetCoverageTool.Ncover3, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover3']")]
        public void ImportDataDotNetCoverage(DotNetCoverageTool tool, string expected)
        {
            ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(ImportType.DotNetCoverage, Path, tool);
            Assert.That(message.ToString(), Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void ImportDataDotNetCoverageNotSupported()
        {
            new ImportDataTeamCityMessage(ImportType.Nunit, Path, DotNetCoverageTool.Ncover3);
        }

        [Test]
        public void TestSuiteStart()
        {
            TestSuiteStartTeamCityMessage message = new TestSuiteStartTeamCityMessage(Name);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testSuiteStarted name='t1']"));
        }

        [Test]
        public void TestSuiteFinish()
        {
            TestSuiteFinishTeamCityMessage message = new TestSuiteFinishTeamCityMessage(Name);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testSuiteFinished name='t1']"));
        }

        [Test]
        public void TestStart()
        {
            TestStartTeamCityMessage message = new TestStartTeamCityMessage(Name, false);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testStarted name='t1']"));
        }

        [Test]
        public void TestStartCaptureStandardOutput()
        {
            TestStartTeamCityMessage message = new TestStartTeamCityMessage(Name, true);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testStarted name='t1' captureStandardOutput='true']"));
        }

        [Test]
        public void TestFinish()
        {
            const double duration = 0.016;
            TestFinishTeamCityMessage message = new TestFinishTeamCityMessage(Name, duration);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testFinished name='t1' duration='16']"));
        }

        [Test]
        public void TestFailed()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testFailed name='t1' message='m1' details='d1']"));
        }

        [Test]
        public void TestFailedFull()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            message.Expected = "e";
            message.Actual = "a";
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='e' actual='a']"));
        }

        [Test]
        public void TestFailedFullEmpty()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            message.Expected = string.Empty;
            message.Actual = string.Empty;
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testFailed name='t1' message='m1' details='d1']"));
        }

        [Test]
        public void TestFailedFullEmptyExpected()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            message.Expected = string.Empty;
            message.Actual = "a";
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' actual='a']"));
        }

        [Test]
        public void TestFailedFullEmptyActual()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            message.Expected = "e";
            message.Actual = string.Empty;
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='e']"));
        }

        [Test]
        public void TestFailedExpectedNotSet()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            Assert.That(message.Expected, Is.Empty);
        }

        [Test]
        public void TestFailedWithExpectedAttr()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = "e" };
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='e']"));
        }

        [Test]
        public void TestFailedWithExpectedAttrOverwrite()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details) { Expected = "e" };
            message.Expected = "o";
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' expected='o']"));
        }

        [Test]
        public void TestFailedActualNotSet()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details);
            Assert.That(message.Actual, Is.Empty);
        }

        [Test]
        public void TestFailedWithActualAttr()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details) { Actual = "e" };
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' actual='e']"));
        }

        [Test]
        public void TestFailedWithActualAttrOverwrite()
        {
            const string msg = "m1";
            const string details = "d1";
            TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(Name, msg, details) { Actual = "e" };
            message.Actual = "o";
            Assert.That(message.ToString(),
                        Is.EqualTo(
                            "##teamcity[testFailed type='comparisonFailure' name='t1' message='m1' details='d1' actual='o']"));
        }

        [Test]
        public void TestIgnored()
        {
            const string msg = "m1";
            TestIgnoredTeamCityMessage message = new TestIgnoredTeamCityMessage(Name, msg);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testIgnored name='t1' message='m1']"));
        }

        [Test]
        public void TestStdOut()
        {
            const string output = "m1";
            TestStdOutTeamCityMessage message = new TestStdOutTeamCityMessage(Name, output);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testStdOut name='t1' out='m1']"));
        }

        [Test]
        public void TestStdErr()
        {
            const string output = "m1";
            TestStdErrTeamCityMessage message = new TestStdErrTeamCityMessage(Name, output);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[testStdErr name='t1' out='m1']"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DotNetCoverageInvalidKey()
        {
            new DotNetCoverMessage("i", "v");
        }

        [Test]
        public void DotNetCoverageNcover3Home()
        {
            const string key = "ncover3_home";
            const string value = "v";
            DotNetCoverMessage message = new DotNetCoverMessage(key, value);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[dotNetCoverage ncover3_home='v']"));
        }

        [TestCase("compilationStarted", "##teamcity[compilationStarted compiler='c']")]
        [TestCase("compilationFinished", "##teamcity[compilationFinished compiler='c']")]
        public void Compilation(string msg, string expected)
        {
            const string compiler = "c";
            CompilationMessage message = new CompilationMessage(compiler, msg);
            Assert.That(message.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void SetParameter()
        {
            SetParameterTeamCityMessage message = new SetParameterTeamCityMessage(Name, Text);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[setParameter name='t1' value='t']"));
        }
        
        [Test]
        public void BuildProblemMandatory()
        {
            BuildProblemMessage message = new BuildProblemMessage(Text);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildProblem description='t']"));
        }
        
        [Test]
        public void BuildProblemAll()
        {
            BuildProblemMessage message = new BuildProblemMessage(Text, Name);
            Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildProblem description='t' identity='t1']"));
        }
    }
}