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
    public class TPartCoverReport : TTask
    {
        private const string XmlReportPth = "path";
        private readonly Mock<ITaskItem> item1;
        private readonly Mock<ITaskItem> item2;
        private readonly PartCoverReport task;

        public TPartCoverReport()
        {
            this.item1 = new Mock<ITaskItem>();
            this.item2 = new Mock<ITaskItem>();
            this.task = new PartCoverReport(this.Logger.Object);
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeTrue();
            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void All()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));
            this.item1.SetupGet(_ => _.ItemSpec).Returns("a");
            this.item2.SetupGet(_ => _.ItemSpec).Returns("b");
            this.task.XmlReportPath = XmlReportPth;
            this.task.ReportXslts = new[] { this.item1.Object, this.item2.Object };
            this.task.Verbose = true;
            this.task.ParseOutOfDate = true;
            this.task.WhenNoDataPublished = "info";
            this.task.Execute().Should().BeTrue();

            this.item1.VerifyGet(_ => _.ItemSpec, Times.Once);
            this.item2.VerifyGet(_ => _.ItemSpec, Times.Once);
            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(2));
        }

        [Fact]
        public void XmlReportPath()
        {
            this.task.XmlReportPath = XmlReportPth;
            this.task.XmlReportPath.Should().Be(XmlReportPth);
        }

        [Fact]
        public void ReportXslts()
        {
            this.task.ReportXslts = new[] { this.item1.Object, this.item2.Object };
            this.task.ReportXslts.ShouldBeEquivalentTo(new[] { this.item1.Object, this.item2.Object });
        }

        [Fact]
        public void InvalidWhenNoDataPublished()
        {
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), It.IsAny<bool>())); // 1
            this.task.WhenNoDataPublished = "bad";
            this.task.Execute().Should().BeFalse();

            this.Logger.Verify(_ => _.LogErrorFromException(It.IsAny<Exception>(), It.IsAny<bool>()), Times.Once);
        }
    }
}