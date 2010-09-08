/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TGoogleTestsPlainImporter
	{
		private static readonly string SuccessTestsPath = Environment.CurrentDirectory + @"\..\..\..\External\GoogleTestsSuccess.xml";
		private static readonly string FailTestsPath = Environment.CurrentDirectory + @"\..\..\..\External\GoogleTestsFailed.xml";

		private Mockery _mockery;
		private ILogger _logger;

		[SetUp]
		public void Setup()
		{
			_mockery = new Mockery();
			_logger = _mockery.NewMock<ILogger>();
		}

		[Test]
		public void ReadSuccessTests()
		{
			Expect.Once.On(_logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			GoogleTestsPlainImporter importer = new GoogleTestsPlainImporter(_logger, false, SuccessTestsPath);

			ExecutionResult result = importer.Import();

			Assert.That(result.Status);
			Assert.That(result.Messages.Count, Is.EqualTo(1));
		}
		
		[Test]
		public void ReadFailTests()
		{
			Expect.Once.On(_logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			GoogleTestsPlainImporter importer = new GoogleTestsPlainImporter(_logger, false, FailTestsPath);

			ExecutionResult result = importer.Import();

			Assert.That(result.Status, Is.False);
			Assert.That(result.Messages.Count, Is.EqualTo(1));
		}
		
		[Test]
		public void ReadFailTestsButContinueOnFailures()
		{
			Expect.Once.On(_logger).GetProperty(TGoogleTestsRunner.HasLoggedErrors).Will(Return.Value(false));

			GoogleTestsPlainImporter importer = new GoogleTestsPlainImporter(_logger, true, FailTestsPath);

			ExecutionResult result = importer.Import();

			Assert.That(result.Status);
			Assert.That(result.Messages.Count, Is.EqualTo(1));
		}
	}
}