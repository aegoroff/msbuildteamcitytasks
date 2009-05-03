/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

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
		}

		[Test]
		public void SimpleMessageAddTimeStamp()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number) { IsAddTimeStamp = true };
			string expected = string.Format("##teamcity[buildNumber '1.0' timestamp='{0}']", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}
		
		[Test]
		public void SimpleMessageFlowId()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number) { IsAddTimeStamp = true, FlowId = "1" };
			string expected = string.Format("##teamcity[buildNumber '1.0' timestamp='{0}' flowId='1']", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
			Assert.That(message.ToString(), Is.EqualTo(expected));
		}
		
		[Test]
		public void SimpleMessageFlowIdToStringTwice()
		{
			const string number = "1.0";
			SimpleTeamCityMessage message = new SimpleTeamCityMessage(BuildNumber, number) { IsAddTimeStamp = true, FlowId = "1" };
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
	}
}