/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
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

		[Test]
		public void BuildStat()
		{
			const string key = "k";
			BuildStatisticTeamCityMessage message = new BuildStatisticTeamCityMessage(key, 10);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildStatisticValue key='k' value='10.00']"));
		}
	
		[Test]
		public void SimpleMessage()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0']"));
			Assert.That(message.MessageText, Is.EqualTo(number));
		}

		[Test]
		public void SimpleMessageAddTimeStamp()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number) { IsAddTimestamp = true };
			string expected = string.Format("##teamcity[buildNumber '1.0' timestamp='{0}']", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}
		
		[Test]
		public void SimpleMessageFlowId()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number) { IsAddTimestamp = true, FlowId = "1" };
			string expected = string.Format("##teamcity[buildNumber '1.0' timestamp='{0}' flowId='1']", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}
		
		[Test]
		public void SimpleMessageFlowIdToStringTwice()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number) { IsAddTimestamp = true, FlowId = "1" };
			string expected = string.Format("##teamcity[buildNumber '1.0' timestamp='{0}' flowId='1']", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
			Assert.That(message.ToString(), Is.EqualTo(expected));
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}

		[Test]
		public void BlockOpen()
		{
			const string name = "b1";
			BlockOpenTeamCityMessage message = new BlockOpenTeamCityMessage(name);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[blockOpened name='b1']"));
		}
		
		[Test]
		public void BlockClose()
		{
			const string name = "b1";
			BlockCloseTeamCityMessage message = new BlockCloseTeamCityMessage(name);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[blockClosed name='b1']"));
		}
		
		[Test]
		public void ReportMessageFull()
		{
			const string text = "t";
			const string error = "e";
			const string status = "ERROR";
			ReportMessageTeamCityMessage message = new ReportMessageTeamCityMessage(text, status, error);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[message text='t' status='ERROR' errorDetails='e']"));
		}
		
		[Test]
		public void ReportMessageStatusAndText()
		{
			const string text = "t";
			const string status = "WARNING";
			ReportMessageTeamCityMessage message = new ReportMessageTeamCityMessage(text, status);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[message text='t' status='WARNING']"));
		}
		
		[Test]
		public void ReportMessageText()
		{
			const string text = "t";
			ReportMessageTeamCityMessage message = new ReportMessageTeamCityMessage(text);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[message text='t']"));
		}

		[Test]
		public void BuildStatus()
		{
			const string text = "t";
			const string status = "SUCCESS";
			BuildStatusTeamCityMessage message = new BuildStatusTeamCityMessage(status, text);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildStatus status='SUCCESS' text='t']"));
		}
		
		[Test]
		public void ImportData()
		{
			const string type = "FxCop";
			const string path = "p";
			ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(type, path);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[importData type='FxCop' path='p']"));
		}

		[TestCase(ImportType.FxCop, "##teamcity[importData type='FxCop' path='p']")]
		[TestCase(ImportType.Junit, "##teamcity[importData type='junit' path='p']")]
		[TestCase(ImportType.Nunit, "##teamcity[importData type='nunit' path='p']")]
		[TestCase(ImportType.Surefire, "##teamcity[importData type='surefire' path='p']")]
		[TestCase(ImportType.Pmd, "##teamcity[importData type='pmd' path='p']")]
		[TestCase(ImportType.FindBugs, "##teamcity[importData type='findBugs' path='p']")]
		[TestCase(ImportType.Mstest, "##teamcity[importData type='mstest' path='p']")]
		public void ImportData(ImportType type, string expected)
		{
			const string path = "p";
			ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(type, path);
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}

		[TestCase(DotNetCoverageTool.PartCover, "##teamcity[importData type='dotNetCoverage' path='p' tool='partcover']")]
		[TestCase(DotNetCoverageTool.Ncover, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover']")]
		[TestCase(DotNetCoverageTool.Ncover3, "##teamcity[importData type='dotNetCoverage' path='p' tool='ncover3']")]
		public void ImportDataDotNetCoverage(DotNetCoverageTool tool, string expected)
		{
			const string path = "p";
			ImportDataTeamCityMessage message = new ImportDataTeamCityMessage(ImportType.DotNetCoverage, path, tool);
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}
		
		[Test]
		[ExpectedException(typeof(NotSupportedException))]
		public void ImportDataDotNetCoverageNotSupported()
		{
			const string path = "p";
			new ImportDataTeamCityMessage(ImportType.Nunit, path, DotNetCoverageTool.Ncover3);
		}

		[Test]
		public void TestSuiteStart()
		{
			const string name = "s1";
			TestSuiteStartTeamCityMessage message = new TestSuiteStartTeamCityMessage(name);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[testSuiteStarted name='s1']"));
		}
		
		[Test]
		public void TestSuiteFinish()
		{
			const string name = "s1";
			TestSuiteFinishTeamCityMessage message = new TestSuiteFinishTeamCityMessage(name);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[testSuiteFinished name='s1']"));
		}

		[Test]
		public void TestStart()
		{
			const string name = "t1";
			TestStartTeamCityMessage message = new TestStartTeamCityMessage(name);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[testStarted name='t1']"));
		}
		
		[Test]
		public void TestFinish()
		{
			const double duration = 0.016;
			const string name = "t1";
			TestFinishTeamCityMessage message = new TestFinishTeamCityMessage(name, duration);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[testFinished name='t1' duration='16']"));
		}
		
		[Test]
		public void TestFailed()
		{
			const string name = "t1";
			const string msg = "m1";
			const string details = "d1";
			TestFailedTeamCityMessage message = new TestFailedTeamCityMessage(name, msg, details);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[testFailed name='t1' message='m1' details='d1']"));
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
	}
}