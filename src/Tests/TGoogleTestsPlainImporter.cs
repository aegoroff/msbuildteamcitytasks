/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using MSBuild.TeamCity.Tasks;
using MSBuild.TeamCity.Tasks.Internal;
using NMock;
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
            var mockery = new MockFactory();
            this.logger = mockery.CreateMock<ILogger>();
        }

        [Fact]
        public void ReadSuccessTests()
        {
            this.logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var importer = new GoogleTestsPlainImporter(this.logger.MockObject, false, successTestsPath);

            importer.Import().Should().BeTrue();
            importer.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void ReadFailTests()
        {
            this.logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var importer = new GoogleTestsPlainImporter(this.logger.MockObject, false, failTestsPath);

            importer.Import().Should().BeFalse();
            importer.Messages.Count.Should().Be(1);
        }

        [Fact]
        public void ReadFailTestsButContinueOnFailures()
        {
            this.logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var importer = new GoogleTestsPlainImporter(this.logger.MockObject, true, failTestsPath);

            importer.Import().Should().BeTrue();
            importer.Messages.Count.Should().Be(1);
        }
    }
}