/*
 * Created by: egr
 * Created at: 09.09.2010
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
    public class TNCover3Report : TTask
    {
        private const string ToolPth = "p";
        private const string XmlReportPth = "path";
        private const string Args = "a";
        private readonly NCover3Report task;

        public TNCover3Report()
        {
            this.task = new NCover3Report(this.Logger.Object);
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.ToolPath = ToolPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(2));
        }

        [Fact]
        public void AllProperties()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.ToolPath = ToolPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.Verbose = true;
            this.task.ParseOutOfDate = true;
            this.task.WhenNoDataPublished = "info";
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(3));
        }

        [Fact]
        public void ToolPath()
        {
            this.task.ToolPath = ToolPth;
            this.task.ToolPath.Should().Be(ToolPth);
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
        public void InvalidWhenNoDataPublished()
        {
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), false));
            this.task.WhenNoDataPublished = "bad";
            this.task.Execute().Should().BeFalse();
            this.Logger.Verify(_ => _.LogErrorFromException(It.IsAny<Exception>(), false), Times.Never());
        }
    }
}