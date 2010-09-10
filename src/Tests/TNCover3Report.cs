/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2010 Alexander Egorov
 */

using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TNCover3Report : TTask
	{
		[Test]
		public void OnlyRequired()
		{
			Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			NCover3Report task = new NCover3Report(Logger)
			{
				ToolPath = "p",
				XmlReportPath = "path"
			};
			Assert.That(task.Execute());
		}

		[Test]
		public void AllProperties()
		{
			Expect.Exactly(3).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			NCover3Report task = new NCover3Report(Logger)
			{
				ToolPath = "p",
				XmlReportPath = "path",
				Arguments = "a"
			};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void ToolPath()
		{
			NCover3Report task = new NCover3Report(Logger)
			{
				ToolPath = "p"
			};
			Assert.That(task.ToolPath, Is.EqualTo("p"));
		}
		
		[Test]
		public void XmlReportPath()
		{
			NCover3Report task = new NCover3Report(Logger)
			{
				XmlReportPath = "path"
			};
			Assert.That(task.XmlReportPath, Is.EqualTo("path"));
		}
		
		[Test]
		public void Arguments()
		{
			NCover3Report task = new NCover3Report(Logger)
			{
				Arguments = "a"
			};
			Assert.That(task.Arguments, Is.EqualTo("a"));
		}
	}
}