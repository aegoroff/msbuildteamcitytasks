/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
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
            this.task = new NCoverReport(this.Logger.MockObject);
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Expects.Exactly(2).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void OnlyRequiredAndArguments()
        {
            this.Logger.Expects.Exactly(3).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void OnlyRequiredAndArgumentsAndReportType()
        {
            this.Logger.Expects.Exactly(4).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.ReportType = RptType;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void Full()
        {
            this.Logger.Expects.Exactly(5).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.ReportType = RptType;
            this.task.ReportOrder = RptOrder;
            this.task.Verbose = true;
            this.task.ParseOutOfDate = true;
            this.task.WhenNoDataPublished = "info";
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void WithException()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null))
                .WithAnyArguments()
                .Will(Throw.Exception(new Exception()));
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            this.task.NCoverExplorerPath = NCoverExplorerPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeFalse();
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
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, false)).WithAnyArguments();
            this.task.WhenNoDataPublished = "bad";
            this.task.Execute().Should().BeFalse();
        }
    }
}