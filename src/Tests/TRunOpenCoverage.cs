/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.IO;
using FluentAssertions;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks;
using NMock;
using Xunit;

namespace Tests
{
    public class TRunOpenCoverage : TTask
    {
        private const string ValidPathToOpenCover = @"..\..\..\packages\OpenCover.4.6.166\tools";
        private const string NUnitPath = @"..\..\..\packages\NUnit.Runners.2.6.4\tools\nunit-console-x86.exe";
        private const string TargetWorkDir = ".";
        private readonly Mock<ITaskItem> item1;
        private readonly Mock<ITaskItem> item2;
        private readonly RunOpenCoverage task;

        public TRunOpenCoverage()
        {
            this.item1 = this.Mockery.CreateMock<ITaskItem>();
            this.item2 = this.Mockery.CreateMock<ITaskItem>();
            this.task = new RunOpenCoverage(this.Logger.MockObject);
        }

        [Fact]
        public void RealRun()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.Normal, null)).WithAnyArguments();
            this.Logger.Expects.Exactly(9).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            this.item1.Expects.Exactly(2)
                .GetProperty(_ => _.ItemSpec)
                .Will(Return.Value("+[MSBuild.TeamCity.Tasks]*ImportData"));
            this.item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("-[System]*"));

            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = NUnitPath;
            var path = new Uri(this.GetType().Assembly.CodeBase).LocalPath;
            this.task.TargetArguments = $"/nologo /noshadow {path} /framework:net-4.0 /run=TGoogleTestArgumentsBuilder";
            this.task.TargetWorkDir = TargetWorkDir;
            this.task.ExcludeByfile = "*.Gen.cs";
            this.task.HideSkipped = "All";
            this.task.SkipAutoProps = true;
            this.task.XmlReportPath = Path.Combine(Path.GetDirectoryName(path), "opencover.xml");

            this.task.Filter = new[] { this.item1.MockObject, this.item2.MockObject };
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void RealRunNoReport()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.Normal, null)).WithAnyArguments();
            this.Logger.Expects.Exactly(9).Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            this.item1.Expects.Exactly(2)
                .GetProperty(_ => _.ItemSpec)
                .Will(Return.Value("+[MSBuild.TeamCity.Tasks]*ImportData"));
            this.item2.Expects.Exactly(2).GetProperty(_ => _.ItemSpec).Will(Return.Value("-[System]*"));

            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = NUnitPath;
            var path = new Uri(this.GetType().Assembly.CodeBase).LocalPath;
            this.task.TargetArguments = $"/nologo /noshadow {path} /framework:net-4.0 /run=TGoogleTestArgumentsBuilder";
            this.task.TargetWorkDir = TargetWorkDir;
            this.task.ExcludeByfile = "*.Gen.cs";
            this.task.HideSkipped = "All";
            this.task.SkipAutoProps = true;

            this.task.Filter = new[] { this.item1.MockObject, this.item2.MockObject };
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void FilterProperty()
        {
            this.task.Filter = new[] { this.item1.MockObject };
            this.task.Filter.ShouldAllBeEquivalentTo(new[] { this.item1.MockObject });
        }

        [Fact]
        public void ToolPath()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.ToolPath = ValidPathToOpenCover;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void ToolPathAndTargetPath()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = TGoogleTestsRunner.correctExePath;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void ToolPathInvalid()
        {
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            this.task.ToolPath = "bad";
            this.task.Execute().Should().BeFalse();
        }
    }
}