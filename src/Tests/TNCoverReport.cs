/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2011 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TNCoverReport : TTask
    {
        private NCoverReport _task;
        private const string NCoverExplorerPth = @"ncoverexplorer\path";
        private const string XmlReportPth = @"path\report";
        private const string Args = "a";
        private const string RptType = "None";
        private const string RptOrder = "1";
        
        [SetUp]
        public void Init()
        {
            _task = new NCoverReport(Logger);
        }
        
        [Test]
        public void OnlyRequired()
        {
            Expect.Exactly(2).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.NCoverExplorerPath = NCoverExplorerPth;
            _task.XmlReportPath = XmlReportPth;
            Assert.That(_task.Execute());
        }

        [Test]
        public void OnlyRequiredAndArguments()
        {
            Expect.Exactly(3).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.NCoverExplorerPath = NCoverExplorerPth;
            _task.XmlReportPath = XmlReportPth;
            _task.Arguments = Args;
            Assert.That(_task.Execute());
        }

        [Test]
        public void OnlyRequiredAndArgumentsAndReportType()
        {
            Expect.Exactly(4).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.NCoverExplorerPath = NCoverExplorerPth;
            _task.XmlReportPath = XmlReportPth;
            _task.Arguments = Args;
            _task.ReportType = RptType;
            Assert.That(_task.Execute());
        }

        [Test]
        public void Full()
        {
            Expect.Exactly(5).On(Logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();
            _task.NCoverExplorerPath = NCoverExplorerPth;
            _task.XmlReportPath = XmlReportPth;
            _task.Arguments = Args;
            _task.ReportType = RptType;
            _task.ReportOrder = RptOrder;
            Assert.That(_task.Execute());
        }

        [Test]
        public void WithException()
        {
            Expect.Once.On(Logger).Method(TTeamCityTaskImplementation.LogMessage).Will(Throw.Exception(new Exception()));
            _task.NCoverExplorerPath = NCoverExplorerPth;
            _task.XmlReportPath = XmlReportPth;
            Assert.That(_task.Execute(), Is.False);
        }

        [Test]
        public void NCoverExplorerPath()
        {
            _task.NCoverExplorerPath = NCoverExplorerPth;
            Assert.That(_task.NCoverExplorerPath, Is.EqualTo(NCoverExplorerPth));
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

        [Test]
        public void ReportType()
        {
            _task.ReportType = RptType;
            Assert.That(_task.ReportType, Is.EqualTo(RptType));
        }

        [Test]
        public void ReportOrder()
        {
            _task.ReportOrder = RptOrder;
            Assert.That(_task.ReportOrder, Is.EqualTo(RptOrder));
        }
    }
}