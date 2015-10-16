/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.IO;
using FluentAssertions;
using MSBuild.TeamCity.Tasks;
using MSBuild.TeamCity.Tasks.Internal;
using NMock;
using Xunit;

namespace Tests
{
    [Collection("SerialTests")]
    public class TGoogleTestsRunner : IDisposable
    {
        internal const string HasLoggedErrors = "HasLoggedErrors";
        internal static readonly string correctExePath = Environment.CurrentDirectory + @"\..\..\..\External\_tst.exe";
        private readonly Mock<ILogger> logger;

        public TGoogleTestsRunner()
        {
            var mockery = new MockFactory();
            this.logger = mockery.CreateMock<ILogger>();
        }

        private static string TestResultPath
        {
            get
            {
                var file = Path.GetFileNameWithoutExtension(correctExePath);
                var dir = Path.GetDirectoryName(Path.GetFullPath(correctExePath));
                return dir + @"\" + file + ".xml";
            }
        }

        public void Dispose()
        {
            DeleteResult();
        }

        internal static void DeleteResult()
        {
            var xmlPath = TestResultPath;

            if (File.Exists(xmlPath))
            {
                File.Delete(xmlPath);
            }
        }

        [Fact]
        public void CorrectExe()
        {
            this.logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var runner = new GoogleTestsRunner(this.logger.MockObject, false, correctExePath);

            runner.Import().Should().BeTrue();
            runner.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void CorrectExeWithExistingFile()
        {
            this.logger.Expects.Exactly(2).GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var runner = new GoogleTestsRunner(this.logger.MockObject, false, correctExePath);
            runner.Import();

            File.Exists(TestResultPath).Should().BeTrue();
            runner.Import().Should().BeTrue();
            runner.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void CorrectExeUsingExecutionTimeout()
        {
            this.logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var runner = new GoogleTestsRunner(this.logger.MockObject, false, correctExePath)
            { ExecutionTimeoutMilliseconds = 200 };
            runner.Import().Should().BeTrue();
            runner.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void IncorrectExe()
        {
            this.logger.Expects.One.Method(_ => _.LogErrorFromException(null, true)).WithAnyArguments();
            this.logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(true));

            var runner = new GoogleTestsRunner(this.logger.MockObject, false, "bad");

            runner.Import().Should().BeFalse();
            runner.Messages.Should().BeEmpty();
        }
    }
}