/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2012 Alexander Egorov
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
        private NCover3Report _task;
        private const string ToolPth = "p";
        private const string XmlReportPth = "path";
        private const string Args = "a";

        [SetUp]
        public void Init()
        {
            _task = new NCover3Report(Logger);
        }

        [Test]
        public void OnlyRequired()
        {
            Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.ToolPath = ToolPth;
            _task.XmlReportPath = XmlReportPth;
            Assert.That(_task.Execute());
        }

        [Test]
        public void AllProperties()
        {
            Expect.Exactly(3).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.ToolPath = ToolPth;
            _task.XmlReportPath = XmlReportPth;
            _task.Arguments = Args;
            Assert.That(_task.Execute());
        }

        [Test]
        public void ToolPath()
        {
            _task.ToolPath = ToolPth;
            Assert.That(_task.ToolPath, Is.EqualTo(ToolPth));
        }

        [Test]
        public void XmlReportPath()
        {
            _task.XmlReportPath = XmlReportPth;
            Assert.That(_task.XmlReportPath, Is.EqualTo(XmlReportPth));
        }

        [Test]
        public void Arguments()
        {
            _task.Arguments = Args;
            Assert.That(_task.Arguments, Is.EqualTo(Args));
        }
    }
}