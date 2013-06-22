/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2013 Alexander Egorov
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
        private NCover3Report task;
        private const string ToolPth = "p";
        private const string XmlReportPth = "path";
        private const string Args = "a";

        [SetUp]
        public void Init()
        {
            task = new NCover3Report(Logger);
        }

        [Test]
        public void OnlyRequired()
        {
            Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ToolPth;
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.Execute());
        }

        [Test]
        public void AllProperties()
        {
            Expect.Exactly(3).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            task.ToolPath = ToolPth;
            task.XmlReportPath = XmlReportPth;
            task.Arguments = Args;
            Assert.That(task.Execute());
        }

        [Test]
        public void ToolPath()
        {
            task.ToolPath = ToolPth;
            Assert.That(task.ToolPath, Is.EqualTo(ToolPth));
        }

        [Test]
        public void XmlReportPath()
        {
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.XmlReportPath, Is.EqualTo(XmlReportPth));
        }

        [Test]
        public void Arguments()
        {
            task.Arguments = Args;
            Assert.That(task.Arguments, Is.EqualTo(Args));
        }
    }
}