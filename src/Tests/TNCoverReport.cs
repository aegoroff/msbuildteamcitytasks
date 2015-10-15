/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using Microsoft.Build.Framework;
using NMock;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TNCoverReport : TTask
    {
        private NCoverReport task;
        private const string NCoverExplorerPth = @"ncoverexplorer\path";
        private const string XmlReportPth = @"path\report";
        private const string Args = "a";
        private const string RptType = "None";
        private const string RptOrder = "1";

        [SetUp]
        public void Init()
        {
            task = new NCoverReport(Logger.MockObject);
        }

        [Test]
        public void OnlyRequired()
        {
            Logger.Expects.Exactly(2).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.NCoverExplorerPath = NCoverExplorerPth;
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.Execute());
        }

        [Test]
        public void OnlyRequiredAndArguments()
        {
            Logger.Expects.Exactly(3).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.NCoverExplorerPath = NCoverExplorerPth;
            task.XmlReportPath = XmlReportPth;
            task.Arguments = Args;
            Assert.That(task.Execute());
        }

        [Test]
        public void OnlyRequiredAndArgumentsAndReportType()
        {
            Logger.Expects.Exactly(4).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.NCoverExplorerPath = NCoverExplorerPth;
            task.XmlReportPath = XmlReportPth;
            task.Arguments = Args;
            task.ReportType = RptType;
            Assert.That(task.Execute());
        }

        [Test]
        public void Full()
        {
            Logger.Expects.Exactly(5).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            task.NCoverExplorerPath = NCoverExplorerPth;
            task.XmlReportPath = XmlReportPth;
            task.Arguments = Args;
            task.ReportType = RptType;
            task.ReportOrder = RptOrder;
            task.Verbose = true;
            task.ParseOutOfDate = true;
            task.WhenNoDataPublished = "info";
            Assert.That(task.Execute());
        }

        [Test]
        public void WithException()
        {
            Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments().Will(Throw.Exception(new Exception()));
            Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            task.NCoverExplorerPath = NCoverExplorerPth;
            task.XmlReportPath = XmlReportPth;
            Assert.That(task.Execute(), Is.False);
        }

        [Test]
        public void NCoverExplorerPath()
        {
            task.NCoverExplorerPath = NCoverExplorerPth;
            Assert.That(task.NCoverExplorerPath, Is.EqualTo(NCoverExplorerPth));
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

        [Test]
        public void ReportType()
        {
            task.ReportType = RptType;
            Assert.That(task.ReportType, Is.EqualTo(RptType));
        }

        [Test]
        public void ReportOrder()
        {
            task.ReportOrder = RptOrder;
            Assert.That(task.ReportOrder, Is.EqualTo(RptOrder));
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