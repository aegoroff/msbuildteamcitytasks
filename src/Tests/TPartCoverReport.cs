/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */


using FluentAssertions;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
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
            this.item1 = this.Mockery.CreateMock<ITaskItem>();
            this.item2 = this.Mockery.CreateMock<ITaskItem>();
            this.task = new PartCoverReport(this.Logger.MockObject);
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void All()
        {
            this.Logger.Expects.Exactly(2).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.item1.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("a"));
            this.item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("b"));
            this.task.XmlReportPath = XmlReportPth;
            this.task.ReportXslts = new[] { this.item1.MockObject, this.item2.MockObject };
            this.task.Verbose = true;
            this.task.ParseOutOfDate = true;
            this.task.WhenNoDataPublished = "info";
            this.task.Execute().Should().BeTrue();
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
            this.task.ReportXslts = new[] { this.item1.MockObject, this.item2.MockObject };
            this.task.ReportXslts.ShouldBeEquivalentTo(new[] { this.item1.MockObject, this.item2.MockObject });
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