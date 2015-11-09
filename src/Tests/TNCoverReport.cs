/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using Microsoft.Build.Framework;
using Moq;
using MSBuild.TeamCity.Tasks;
using Xunit;

namespace Tests
{
    public class TNCoverReport : TTask
    {
        private const string NCoverExplorerPth = @"ncoverexplorer\path";
        private const string XmlReportPth = @"path\report";
        private const string Args = "a";
        private const string RptType = "None";
        private const string RptOrder = "1";
        private readonly NCoverReport task;

        public TNCoverReport()
        {
            this.task = new NCoverReport(this.Logger.Object);
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(2));
        }

        [Fact]
        public void OnlyRequiredAndArguments()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(3));
        }

        [Fact]
        public void OnlyRequiredAndArgumentsAndReportType()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.ReportType = RptType;
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(4));
        }

        [Fact]
        public void Full()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.ReportType = RptType;
            this.task.ReportOrder = RptOrder;
            this.task.Verbose = true;
            this.task.ParseOutOfDate = true;
            this.task.WhenNoDataPublished = "info";
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(5));
        }

        [Fact]
        public void WithException()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())).Throws<Exception>();
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), true));
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeFalse();

            this.Logger.Verify(_ => _.LogErrorFromException(It.IsAny<Exception>(), true), Times.Once);
        }

        [Fact]
        public void NCoverExplorerPath()
        {
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.NCoverExplorerPath.Should().Be(NCoverExplorerPth);
        }

        [Fact]
        public void XmlReportPath()
        {
            this.task.XmlReportPath = XmlReportPth;
            this.task.XmlReportPath.Should().Be(XmlReportPth);
        }

        [Fact]
        public void Arguments()
        {
            this.task.Arguments = Args;
            this.task.Arguments.Should().Be(Args);
        }

        [Fact]
        public void ReportType()
        {
            this.task.ReportType = RptType;
            this.task.ReportType.Should().Be(RptType);
        }

        [Fact]
        public void ReportOrder()
        {
            this.task.ReportOrder = RptOrder;
            this.task.ReportOrder.Should().Be(RptOrder);
        }

        [Fact]
        public void InvalidWhenNoDataPublished()
        {
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), false));
            this.task.WhenNoDataPublished = "bad";
            this.task.Execute().Should().BeFalse();

            this.Logger.Verify(_ => _.LogErrorFromException(It.IsAny<Exception>(), false), Times.Never);
        }
    }
}