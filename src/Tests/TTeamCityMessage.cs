/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2011 Alexander Egorov
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

        [TestCase( ImportType.FxCop, "##teamcity[importData type='FxCop' path='p']" )]
        [TestCase( ImportType.Junit, "##teamcity[importData type='junit' path='p']" )]
        [TestCase( ImportType.Nunit, "##teamcity[importData type='nunit' path='p']" )]
        [TestCase( ImportType.Surefire, "##teamcity[importData type='surefire' path='p']" )]
        [TestCase( ImportType.Pmd, "##teamcity[importData type='pmd' path='p']" )]
        [TestCase( ImportType.FindBugs, "##teamcity[importData type='findBugs' path='p']" )]
        [TestCase( ImportType.Mstest, "##teamcity[importData type='mstest' path='p']" )]
        public void ImportData( ImportType type, string expected )
        {
            ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(type, Path);
            Assert.That(message.ToString(), Is.EqualTo(expected));
        }

        [TestCase( DotNetCoverageTool.PartCover,
            "##teamcity[importData type='dotNetCoverage' path='p' tool='partcover']" )]
        [TestCase( DotNetCoverageTool.Ncover, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']" )]
        [TestCase( DotNetCoverageTool.Ncover3, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover3']" )]
        public void ImportDataDotNetCoverage( DotNetCoverageTool tool, string expected )
        {
            ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(ImportType.DotNetCoverage, Path, tool);
            Assert.That(message.ToString(), Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException( typeof(NotSupportedException) )]
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
        [ExpectedException( typeof(ArgumentException) )]
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
    }
}