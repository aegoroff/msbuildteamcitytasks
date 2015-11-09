/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using Moq;
using MSBuild.TeamCity.Tasks;
using MSBuild.TeamCity.Tasks.Internal;
using Xunit;

namespace Tests
{
    public class TGoogleTestsPlainImporter
    {
        internal static readonly string successTestsPath = Environment.CurrentDirectory +
                                                           @"\..\..\..\External\GoogleTestsSuccess.xml";

        private static readonly string failTestsPath = Environment.CurrentDirectory +
                                                       @"\..\..\..\External\GoogleTestsFailed.xml";

        private readonly Mock<ILogger> logger;

        public TGoogleTestsPlainImporter()
        {
            this.logger = new Mock<ILogger>();
        }

        [Fact]
        public void ReadSuccessTests()
        {
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(false);

            var importer = new GoogleTestsPlainImporter(this.logger.Object, false, successTestsPath);

            importer.Import().Should().BeTrue();
            importer.Messages.Count.Should().Be(1);

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Once());
        }

        [Fact]
        public void ReadFailTests()
        {
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(It.IsAny<bool>());

            var importer = new GoogleTestsPlainImporter(this.logger.Object, false, failTestsPath);

            importer.Import().Should().BeFalse();
            importer.Messages.Count.Should().Be(1);

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Never());
        }

        [Fact]
        public void ReadFailTestsButContinueOnFailures()
        {
            this.logger.SetupGet(_ => _.HasLoggedErrors).Returns(false);

            var importer = new GoogleTestsPlainImporter(this.logger.Object, true, failTestsPath);

            importer.Import().Should().BeTrue();
            importer.Messages.Count.Should().Be(1);

            this.logger.VerifyGet(_ => _.HasLoggedErrors, Times.Once());
        }
    }
}