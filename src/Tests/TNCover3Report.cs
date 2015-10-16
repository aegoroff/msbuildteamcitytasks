/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */

using FluentAssertions;
using Microsoft.Build.Framework;
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
            this.task = new NCover3Report(this.Logger.MockObject);
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Expects.Exactly(2).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.ToolPath = ToolPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void AllProperties()
        {
            this.Logger.Expects.Exactly(3).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.ToolPath = ToolPth;
            this.task.XmlReportPath = XmlReportPth;
            this.task.Arguments = Args;
            this.task.Verbose = true;
            this.task.ParseOutOfDate = true;
            this.task.WhenNoDataPublished = "info";
            this.task.Execute().Should().BeTrue();
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
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, false)).WithAnyArguments();
            this.task.WhenNoDataPublished = "bad";
            this.task.Execute().Should().BeFalse();
        }
    }
}