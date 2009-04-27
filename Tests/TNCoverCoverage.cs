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
	public class TNCoverCoverage
	{
		[Test]
		public void NcoverReportPathProperty()
		{
			var task = new NCoverCoverage { NcoverReportPath = "r.xml" };
			Assert.That(task.NcoverReportPath, Is.EqualTo("r.xml"));
		}
		
		[Test]
		public void Execute()
		{
			var task = new NCoverCoverage { NcoverReportPath = @"D:\CSharp\NCover2TeamCity\CoverageSummary.xml" };
			Assert.That(task.Execute());
		}
	}
}