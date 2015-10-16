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
    [Collection("SerialTests")]
    public class TRunGoogleTests : TTask
    {
        private const string TestFilter = "*";
        private const int ExecutionTimeoutMilliseconds = 200;
        private readonly RunGoogleTests task;

        public TRunGoogleTests()
        {
            this.task = new RunGoogleTests(this.Logger.MockObject);
        }

        protected override void AfterTeardown()
        {
            TGoogleTestsRunner.DeleteResult();
        }

        [Fact]
        public void OnlyRequired()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            this.task.TestExePath = TGoogleTestsRunner.correctExePath;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void BadPath()
        {
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            this.Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(true));

            this.task.TestExePath = "bad";
            this.task.Execute().Should().BeFalse();
        }

        [Fact]
        public void ContinueOnFailures()
        {
            this.Logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            this.Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            this.task.TestExePath = "bad";
            this.task.ContinueOnFailures = true;
            this.task.Execute().Should().BeTrue();
        }

        [Fact]
        public void AllPropertiesSet()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

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