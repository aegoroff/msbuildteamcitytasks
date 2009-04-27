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
		public void BuildNumber()
		{
			const string number = "1.0";
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(number);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[buildNumber '1.0']"));
		}
		
		[Test]
		public void PublishArtifacts()
		{
			const string path = "1.msi";
			PublishArtifactTeamCityMessage message = new PublishArtifactTeamCityMessage(path);
			Assert.That(message.ToString(), Is.EqualTo("##teamcity[publishArtifacts '1.msi']"));
		}
	}
}