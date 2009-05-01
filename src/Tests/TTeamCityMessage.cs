/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Tests
{
	[TestFixture]
	public class TTeamCityMessage
	{
		[Test]
		public void ToStringAfterEmptyCtor()
		{
			TeamCityMessage message = new TeamCityMessage();
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[]"));
		}
		
		[Test]
		public void ToStringAfterNormalCtor()
		{
			const string m = "m";
			TeamCityMessage message = new TeamCityMessage(m);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[m]"));
		}
		
		[Test]
		public void BuildStat()
		{
			const string key = "k";
			BuildStatisticTeamCityMessage message = new BuildStatisticTeamCityMessage(key, 10);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildStatisticValue key='k' value='10.00']"));
		}
		
		[Test]
		public void BuildStatEscapeAll()
		{
			const string key = "k\r\n|']";
			BuildStatisticTeamCityMessage message = new BuildStatisticTeamCityMessage(key, 10);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildStatisticValue key='k|r|n|||'|]' value='10.00']"));
		}
		
		[Test]
		public void BuildNumber()
		{
			const string number = "1.0";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0']"));
		}
		
		[Test]
		public void BuildNumberEscapeApos()
		{
			const string number = "1.0'";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0|'']"));
		}
		
		[Test]
		public void BuildNumberEscapeClosingBracket()
		{
			const string number = "1.0]";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0|]']"));
		}
		
		[Test]
		public void BuildNumberEscapeVerticalLine()
		{
			const string number = "1.0|";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0||']"));
		}
		
		[Test]
		public void BuildNumberEscapeN()
		{
			const string number = "1.0\n";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0|n']"));
		}
		
		[Test]
		public void BuildNumberEscapeR()
		{
			const string number = "1.0\r";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0|r']"));
		}
		
		[Test]
		public void BuildNumberEscapeAll()
		{
			const string number = "1.0\r\n|']";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0|r|n|||'|]']"));
		}
		
		[Test]
		public void PublishArtifacts()
		{
			const string path = "1.msi";
			PublishArtifactTeamCityMessage message = new PublishArtifactTeamCityMessage(path);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[publishArtifacts '1.msi']"));
		}
		
		[Test]
		public void PublishArtifactsEscapeAll()
		{
			const string path = "1.msi\r\n|']";
			PublishArtifactTeamCityMessage message = new PublishArtifactTeamCityMessage(path);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[publishArtifacts '1.msi|r|n|||'|]']"));
		}
	}
}