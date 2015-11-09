/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.IO;
using FluentAssertions;
using Moq;
using MSBuild.TeamCity.Tasks;
using MSBuild.TeamCity.Tasks.Internal;
using Xunit;

namespace Tests
{
    [Collection("SerialTests")]
    public class TGoogleTestsRunner : IDisposable
    {
        internal static readonly string correctExePath = Environment.CurrentDirectory + @"\..\..\..\External\_tst.exe";
        private readonly Mock<ILogger> logger;

        public TGoogleTestsRunner()
        {
            this.logger = new Mock<ILogger>();
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
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(false); // 1

            var runner = new GoogleTestsRunner(this.logger.Object, false, correctExePath);

            runner.Import().Should().BeTrue();
            runner.Messages.Count.Should().Be(1);

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Once);
        }

        [Fact]
        public void CorrectExeWithExistingFile()
        {
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(false); // 2

            var runner = new GoogleTestsRunner(this.logger.Object, false, correctExePath);
            runner.Import();

            File.Exists(TestResultPath).Should().BeTrue();
            runner.Import().Should().BeTrue();
            runner.Messages.Count.Should().Be(1);

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Exactly(2));
        }

        [Fact]
        public void CorrectExeUsingExecutionTimeout()
        {
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(false); // 1

            var runner = new GoogleTestsRunner(this.logger.Object, false, correctExePath)
            { ExecutionTimeoutMilliseconds = 200 };
            runner.Import().Should().BeTrue();
            runner.Messages.Count.Should().Be(1);

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Once);
        }

        [Fact]
        public void IncorrectExe()
        {
            this.logger.Setup(_ => _.LogErrorFromException(It.IsAny<Exception>(), true)); // 1
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(true); // 0

            var runner = new GoogleTestsRunner(this.logger.Object, false, "bad");

            runner.Import().Should().BeFalse();
            runner.Messages.Should().BeEmpty();

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Never);
        }
    }
}