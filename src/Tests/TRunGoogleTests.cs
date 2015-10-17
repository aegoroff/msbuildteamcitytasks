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
    [Collection("SerialTests")]
    public class TRunGoogleTests : TTask
    {
        private const string TestFilter = "*";
        private const int ExecutionTimeoutMilliseconds = 200;
        private readonly RunGoogleTests task;

        public TRunGoogleTests()
        {
            this.task = new RunGoogleTests(this.Logger.Object);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(true);
            if (disposing)
            {
                TGoogleTestsRunner.DeleteResult();
            }
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 1
            this.Logger.SetupGet(_ => _.HasLoggedErrors).Returns(false); // 1

            this.task.TestExePath = TGoogleTestsRunner.correctExePath;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void BadPath()
        {
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), true)); // 1
            this.Logger.SetupGet(_ => _.HasLoggedErrors).Returns(true); // 1

            this.task.TestExePath = "bad";
            this.task.Execute().Should().BeFalse();
        }

        [Fact]
        public void ContinueOnFailures()
        {
            this.Logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), true)); // 1
            this.Logger.SetupGet(_ => _.HasLoggedErrors).Returns(false); // 1

            this.task.TestExePath = "bad";
            this.task.ContinueOnFailures = true;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void AllPropertiesSet()
        {
            this.Logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 1
            this.Logger.SetupGet(_ => _.HasLoggedErrors).Returns(false); // 1

            this.task.TestExePath = TGoogleTestsRunner.correctExePath;
            this.task.ContinueOnFailures = false;
            this.task.RunDisabledTests = true;
            this.task.CatchGtestExceptions = true;
            this.task.TestFilter = TestFilter;
            this.task.ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
            this.task.Verbose = true;
            this.task.WhenNoDataPublished = "error";
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestExePathProperty()
        {
            this.task.TestExePath = TGoogleTestsRunner.correctExePath;
            this.task.TestExePath.Should().Be(TGoogleTestsRunner.correctExePath);
        }

        [Fact]
        public void ContinueOnFailuresProperty()
        {
            this.task.ContinueOnFailures = true;
            this.task.ContinueOnFailures.Should().BeTrue();
        }

        [Fact]
        public void RunDisabledTestsProperty()
        {
            this.task.RunDisabledTests = true;
            this.task.RunDisabledTests.Should().BeTrue();
        }

        [Fact]
        public void CatchGtestExceptionsProperty()
        {
            this.task.CatchGtestExceptions = true;
            this.task.CatchGtestExceptions.Should().BeTrue();
        }

        [Fact]
        public void TestFilterProperty()
        {
            this.task.TestFilter = TestFilter;
            this.task.TestFilter.Should().Be(TestFilter);
        }

        [Fact]
        public void ExecutionTimeoutMillisecondsProperty()
        {
            this.task.ExecutionTimeoutMilliseconds = ExecutionTimeoutMilliseconds;
            this.task.ExecutionTimeoutMilliseconds.Should().Be(ExecutionTimeoutMilliseconds);
        }
    }
}