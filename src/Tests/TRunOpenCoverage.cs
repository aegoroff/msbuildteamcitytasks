/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.IO;
using FluentAssertions;
using Microsoft.Build.Framework;
using Moq;
using MSBuild.TeamCity.Tasks;
using Xunit;

namespace Tests
{
    [Collection("SerialTests")]
    public class TRunOpenCoverage : TTask
    {
        private const string ValidPathToOpenCover = @"..\..\..\packages\OpenCover.4.6.166\tools";
        private const string XUnitPath = @"..\..\..\packages\xunit.runner.console.2.1.0\tools\xunit.console.exe";
        private const string TargetWorkDir = ".";
        private readonly Mock<ITaskItem> item1;
        private readonly Mock<ITaskItem> item2;
        private readonly RunOpenCoverage task;

        public TRunOpenCoverage()
        {
            this.item1 = new Mock<ITaskItem>();
            this.item2 = new Mock<ITaskItem>();
            this.task = new RunOpenCoverage(this.Logger.Object);
        }

        [Theory]
        [InlineData("opencover.xml")]
        [InlineData(null)]
        [InlineData("bad")]
        public void RealRun(string report)
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));

            this.item1.SetupGet(_ => _.ItemSpec).Returns("+[MSBuild.TeamCity.Tasks]*ImportData"); // 1
            this.item2.SetupGet(_ => _.ItemSpec).Returns("-[System]*"); // 1

            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = XUnitPath;
            var path = new Uri(this.GetType().Assembly.CodeBase).LocalPath;
            this.task.TargetArguments = $"{path} -nologo -noshadow -class TGoogleTestArgumentsBuilder";
            this.task.TargetWorkDir = TargetWorkDir;
            this.task.ExcludeByfile = "*.Gen.cs";
            this.task.HideSkipped = "All";
            this.task.SkipAutoProps = true;

            this.task.XmlReportPath = report != null ? Path.Combine(Path.GetDirectoryName(path), "opencover.xml") : null;

            this.task.Filter = new[] { this.item1.Object, this.item2.Object };
            this.task.Execute().Should().BeTrue();

            this.item1.VerifyGet(_ => _.ItemSpec, Times.Once);
            this.item2.VerifyGet(_ => _.ItemSpec, Times.Once);
            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMost(9));
        }

        [Fact]
        public void FilterProperty()
        {
            this.task.Filter = new[] { this.item1.Object };
            this.task.Filter.ShouldAllBeEquivalentTo(new[] { this.item1.Object });
        }

        [Fact]
        public void ToolPath()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 0
            this.task.ToolPath = ValidPathToOpenCover;
            this.task.Execute().Should().BeTrue();
            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ToolPathAndTargetPath()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 0
            this.task.ToolPath = ValidPathToOpenCover;
            this.task.TargetPath = TGoogleTestsRunner.correctExePath;
            this.task.Execute().Should().BeTrue();

            this.Logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ToolPathInvalid()
        {
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), true)); // 1
            this.task.ToolPath = "bad";
            this.task.Execute().Should().BeFalse();

            this.Logger.Verify(_ => _.LogErrorFromException(It.IsAny<Exception>(), true), Times.Once);
        }
    }
}