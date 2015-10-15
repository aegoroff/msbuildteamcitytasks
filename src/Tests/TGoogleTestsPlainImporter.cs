/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using MSBuild.TeamCity.Tasks.Internal;
using NMock;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TGoogleTestsPlainImporter
    {
        internal static readonly string SuccessTestsPath = Environment.CurrentDirectory +
                                                           @"\..\..\..\External\GoogleTestsSuccess.xml";

        private static readonly string FailTestsPath = Environment.CurrentDirectory +
                                                       @"\..\..\..\External\GoogleTestsFailed.xml";

        private MockFactory mockery;
        private Mock<ILogger> logger;

        [SetUp]
        public void Setup()
        {
            mockery = new MockFactory();
            logger = mockery.CreateMock<ILogger>();
        }

        [Test]
        public void ReadSuccessTests()
        {
            logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var importer = new GoogleTestsPlainImporter(logger.MockObject, false, SuccessTestsPath);

            Assert.That(importer.Import());
            Assert.That(importer.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void ReadFailTests()
        {
            logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var importer = new GoogleTestsPlainImporter(logger.MockObject, false, FailTestsPath);

            Assert.That(importer.Import(), Is.False);
            Assert.That(importer.Messages.Count, Is.EqualTo(1));
        }

        [Test]
        public void ReadFailTestsButContinueOnFailures()
        {
            logger.Expects.One.GetProperty(_ => _.HasLoggedErrors).Will(Return.Value(false));

            var importer = new GoogleTestsPlainImporter(logger.MockObject, true, FailTestsPath);

            Assert.That(importer.Import());
            Assert.That(importer.Messages.Count, Is.EqualTo(1));
        }
    }
}