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
    public class TSimpleTasks : TTask
    {
        public TSimpleTasks()
        {
            this.Logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
        }

        [Fact]
        public void CommonPropertiesTest()
        {
            var task = new BlockOpen(this.Logger.MockObject)
            {
                Name = "n",
                FlowId = "1",
                IsAddTimestamp = true
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void BlockTaskNameProperty()
        {
            var task = new BlockOpen(this.Logger.MockObject)
            {
                Name = "n"
            };
            task.Name.Should().Be("n");
        }

        [Fact]
        public void BlockOpen()
        {
            var task = new BlockOpen(this.Logger.MockObject)
            {
                Name = "n"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void BlockClose()
        {
            var task = new BlockClose(this.Logger.MockObject)
            {
                Name = "n"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void BuildNumber()
        {
            var task = new BuildNumber(this.Logger.MockObject)
            {
                Number = "1"
            };
            task.Execute().Should().BeTrue();
            task.Number.Should().Be("1");
        }

        [Fact]
        public void BuildProgressStart()
        {
            var task = new BuildProgressStart(this.Logger.MockObject)
            {
                Message = "m"
            };
            task.Execute().Should().BeTrue();
            task.Message.Should().Be("m");
        }

        [Fact]
        public void BuildProgressMessage()
        {
            var task = new BuildProgressMessage(this.Logger.MockObject)
            {
                Message = "m"
            };
            task.Execute().Should().BeTrue();
            task.Message.Should().Be("m");
        }

        [Fact]
        public void BuildProgressFinish()
        {
            var task = new BuildProgressFinish(this.Logger.MockObject)
            {
                Message = "m"
            };
            task.Execute().Should().BeTrue();
            task.Message.Should().Be("m");
        }

        [Fact]
        public void BuildStatus()
        {
            var task = new BuildStatus(this.Logger.MockObject)
            {
                Status = "SUCCESS",
                Text = "t"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void ReportMessage()
        {
            var task = new ReportMessage(this.Logger.MockObject)
            {
                Status = "WARNING",
                Text = "t",
                ErrorDetails = "ed"
            };
            task.Execute().Should().BeTrue();
            task.Status.Should().Be("WARNING");
            task.Text.Should().Be("t");
            task.ErrorDetails.Should().Be("ed");
        }

        [Fact]
        public void ReportBuildStatistic()
        {
            var task = new ReportBuildStatistic(this.Logger.MockObject)
            {
                Key = "k",
                Value = 1
            };
            task.Execute().Should().BeTrue();
            task.Key.Should().Be("k");
            task.Value.Should().Be(1);
        }

        [Theory]
        [InlineData("dotNetCoverage", "ncover", false, false, null)]
        [InlineData("mstest", null, false, false, null)]
        [InlineData("dotNetCoverage", "ncover", true, false, null)]
        [InlineData("dotNetCoverage", "ncover", false, true, null)]
        [InlineData("dotNetCoverage", "ncover", true, true, null)]
        [InlineData("mstest", null, false, false, "info")]
        [InlineData("mstest", null, false, false, "nothing")]
        [InlineData("mstest", null, false, false, "warning")]
        [InlineData("mstest", null, false, false, "error")]
        public void ImportData(string type, string tool, bool verbose, bool parseOutOfDate, string whenNoDataPublished)
        {
            var task = new ImportData(this.Logger.MockObject)
            {
                Path = "p",
                Type = type,
                Tool = tool,
                Verbose = verbose,
                ParseOutOfDate = parseOutOfDate,
                WhenNoDataPublished = whenNoDataPublished
            };
            task.Execute().Should().BeTrue();
            task.Path.Should().Be("p");
            task.Type.Should().Be(type);
            task.Tool.Should().Be(tool);
        }

        [Theory]
        [InlineData("bad", null, false, false, null)]
        [InlineData("mstest", "ncover", false, false, "bad")]
        [InlineData("mstest", null, false, false, "bad")]
        public void ImportDataExceptions(string type, string tool, bool verbose, bool parseOutOfDate, string whenNoDataPublished)
        {
            Assert.Throws<UnexpectedInvocationException>(
                delegate
                {
                    var task = new ImportData(this.Logger.MockObject)
                    {
                        Path = "p",
                        Type = type,
                        Tool = tool,
                        Verbose = verbose,
                        ParseOutOfDate = parseOutOfDate,
                        WhenNoDataPublished = whenNoDataPublished
                    };
                    task.Execute().Should().BeTrue();
                    task.Path.Should().Be("p");
                    task.Type.Should().Be(type);
                    task.Tool.Should().Be(tool);
                });
        }

        [Fact]
        public void ImportGoogleTests()
        {
            this.Logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var task = new ImportGoogleTests(this.Logger.MockObject)
            {
                ContinueOnFailures = true,
                TestResultsPath = TGoogleTestsPlainImporter.successTestsPath,
                Verbose = true,
                WhenNoDataPublished = "error",
                ParseOutOfDate = true
            };
            task.Execute().Should().BeTrue();
            task.ContinueOnFailures.Should().BeTrue();
            task.TestResultsPath.Should().Be(TGoogleTestsPlainImporter.successTestsPath);
        }

        [Fact]
        public void EnableServiceMessages()
        {
            var task = new EnableServiceMessages(this.Logger.MockObject);
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void DisableServiceMessages()
        {
            var task = new DisableServiceMessages(this.Logger.MockObject);
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void CompilationStarted()
        {
            var task = new CompilationStarted(this.Logger.MockObject);
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void CompilationFinished()
        {
            var task = new CompilationFinished(this.Logger.MockObject);
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestSuiteStarted()
        {
            var task = new TestSuiteStarted(this.Logger.MockObject)
            {
                Name = "n"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestSuiteFinished()
        {
            var task = new TestSuiteFinished(this.Logger.MockObject)
            {
                Name = "n"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestStarted()
        {
            var task = new TestStarted(this.Logger.MockObject)
            {
                Name = "n"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestStartedCaptureStandardOutput()
        {
            var task = new TestStarted(this.Logger.MockObject)
            {
                Name = "n",
                CaptureStandardOutput = true
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestFinished()
        {
            var task = new TestFinished(this.Logger.MockObject)
            {
                Name = "n",
                Duration = 3.0
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestIgnored()
        {
            var task = new TestIgnored(this.Logger.MockObject)
            {
                Name = "n",
                Message = "Comment"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestStdOut()
        {
            var task = new TestStdOut(this.Logger.MockObject)
            {
                Name = "n",
                Out = "out"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestStdErr()
        {
            var task = new TestStdErr(this.Logger.MockObject)
            {
                Name = "n",
                Out = "out"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestFailedRequired()
        {
            var task = new TestFailed(this.Logger.MockObject)
            {
                Name = "n",
                Message = "m",
                Details = "d"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void TestFailedAll()
        {
            var task = new TestFailed(this.Logger.MockObject)
            {
                Name = "n",
                Message = "m",
                Details = "d",
                Actual = "1",
                Expected = "2"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void SetParameter()
        {
            var task = new SetParameter(this.Logger.MockObject)
            {
                Name = "n",
                Value = "v"
            };
            task.Execute().Should().BeTrue();
        }

        [Fact]
        public void BuildProblem()
        {
            var task = new BuildProblem(this.Logger.MockObject)
            {
                Description = "d",
                Identity = "i"
            };
            task.Execute().Should().BeTrue();
        }
    }
}