/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2013 Alexander Egorov
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
        private ITaskItem item1;
        private ITaskItem item2;
        private const string ItemSpec = "ItemSpec";
        private PartCoverReport task;
        private const string XmlReportPth = "path";


        [SetUp]
        public void ThisSetup()
        {
            Setup();
            item1 = Mockery.NewMock<ITaskItem>();
            item2 = Mockery.NewMock<ITaskItem>();
            task = new PartCoverReport(Logger);
        }

        [Test]
        public void OnlyRequired()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.Execute());
        }

        [Test]
        public void All()
        {
            Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            Expect.Exactly(2).On(item1).GetProperty(ItemSpec).Will(Return.Value("a"));
            Expect.Exactly(2).On(item2).GetProperty(ItemSpec).Will(Return.Value("b"));
            task.XmlReportPath = XmlReportPth;
            task.ReportXslts = new[] { item1, item2 };
            Assert.That(task.Execute());
        }

        [Test]
        public void XmlReportPath()
        {
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.XmlReportPath, Is.EqualTo(XmlReportPth));
        }

        [Test]
        public void ReportXslts()
        {
            task.ReportXslts = new[] { item1, item2 };
            Assert.That(task.ReportXslts, Is.EquivalentTo(new[] { item1, item2 }));
        }
    }
}