/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2010 Alexander Egorov
 */


using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TPartCoverReport : TTask
	{

		private ITaskItem _item1;
		private ITaskItem _item2;
		private const string ItemSpec = "ItemSpec";


		[SetUp]
		public void ThisSetup()
		{
			Setup();
			_item1 = Mockery.NewMock<ITaskItem>();
			_item2 = Mockery.NewMock<ITaskItem>();
		}
		
		[Test]
		public void OnlyRequired()
		{
			Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			PartCoverReport task = new PartCoverReport(Logger)
			{
				XmlReportPath = "path"
			};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void All()
		{
			Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
			Expect.Once.On(_item1).GetProperty(ItemSpec).Will(Return.Value("a"));
			Expect.Once.On(_item2).GetProperty(ItemSpec).Will(Return.Value("b"));

			PartCoverReport task = new PartCoverReport(Logger)
			{
				XmlReportPath = "path",
				ReportXslts = new[] { _item1, _item2 }
			};
			Assert.That(task.Execute(), Is.False); // TODO: fix it
		}
		
		[Test]
		public void XmlReportPath()
		{
			PartCoverReport task = new PartCoverReport(Logger)
			{
				XmlReportPath = "path"
			};
			Assert.That(task.XmlReportPath, Is.EqualTo("path"));
		}
		
		[Test]
		public void ReportXslts()
		{
			PartCoverReport task = new PartCoverReport(Logger)
			{
				ReportXslts = new[] { _item1, _item2 }
			};
			Assert.That(task.ReportXslts, Is.EquivalentTo(new[] { _item1, _item2 }));
		}
	}
}