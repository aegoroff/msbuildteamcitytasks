/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */


using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TPartCoverReport : TTask
    {
        private Mock<ITaskItem> item1;
        private Mock<ITaskItem> item2;
        private PartCoverReport task;
        private const string XmlReportPth = "path";

        protected override void AfterSetup()
        {
            item1 = Mockery.CreateMock<ITaskItem>();
            item2 = Mockery.CreateMock<ITaskItem>();
            task = new PartCoverReport(Logger.MockObject);
        }

        [Test]
        public void OnlyRequired()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.Execute());
        }

        [Test]
        public void All()
        {
            Logger.Expects.Exactly(2).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            item1.Expects.Exactly(2).GetProperty(_=>_.ItemSpec).Will(Return.Value("a"));
            item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));
            task.XmlReportPath = XmlReportPth;
            task.ReportXslts = new[] { item1.MockObject, item2.MockObject };
            task.Verbose = true;
            task.ParseOutOfDate = true;
            task.WhenNoDataPublished = "info";
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
            task.ReportXslts = new[] { item1.MockObject, item2.MockObject };
            Assert.That(task.ReportXslts, Is.EquivalentTo(new[] { item1.MockObject, item2.MockObject }));
        }

        [Test]
        public void InvalidWhenNoDataPublished()
        {
            Logger.Expects.One.Method(_ => _.LogErrorFromException(null, false)).WithAnyArguments();
            task.WhenNoDataPublished = "bad";
            Assert.That(task.Execute(), Is.False);
        }
    }
}