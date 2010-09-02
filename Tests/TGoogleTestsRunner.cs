/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System;
using System.IO;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TGoogleTestsRunner
	{
		private static readonly string CorrectExePath = Environment.CurrentDirectory + @"\..\..\..\External\_tst.exe";

		internal const string HasLoggedErrors = "HasLoggedErrors";
		internal const string LogError = "LogError";

		private Mockery _mockery;
		private ILogger _logger;

		[SetUp]
		public void Setup()
		{
			_mockery = new Mockery();
			_logger = _mockery.NewMock<ILogger>();
		}

		[TearDown]
		public void TearDown()
		{
			string xmlPath = TestResultPath;

			if ( File.Exists(xmlPath) )
			{
				File.Delete(xmlPath);
			}
		}

		private static string TestResultPath
		{
			get
			{
				string file = Path.GetFileNameWithoutExtension(CorrectExePath);
				string dir = Path.GetDirectoryName(Path.GetFullPath(CorrectExePath));
				return dir + @"\" + file + ".xml";
			}
		}

		[Test]
		public void CorrectExe()
		{
			Expect.Once.On(_logger).GetProperty(HasLoggedErrors).Will(Return.Value(false));

			GoogleTestsRunner runner = new GoogleTestsRunner(_logger, false, CorrectExePath);
			ExecutionResult result = runner.Import();

			Assert.That(result.Status);
			Assert.That(result.Message, Is.Not.Null);
		}

		[Test]
		public void CorrectExeWithExistingFile()
		{
			Expect.Exactly(2).On(_logger).GetProperty(HasLoggedErrors).Will(Return.Value(false));

			GoogleTestsRunner runner = new GoogleTestsRunner(_logger, false, CorrectExePath);
			runner.Import();

			Assert.That(File.Exists(TestResultPath));

			ExecutionResult result = runner.Import();

			Assert.That(result.Status);
			Assert.That(result.Message, Is.Not.Null);
		}

		[Test]
		public void CorrectExeUsingExecutionTimeout()
		{
			Expect.Once.On(_logger).GetProperty(HasLoggedErrors).Will(Return.Value(false));

			GoogleTestsRunner runner = new GoogleTestsRunner(_logger, false, CorrectExePath)
			                           	{ ExecutionTimeoutMilliseconds = 200 };
			ExecutionResult result = runner.Import();

			Assert.That(result.Status);
			Assert.That(result.Message, Is.Not.Null);
		}

		[Test]
		public void IncorrectExe()
		{
			Expect.Once.On(_logger).GetProperty(HasLoggedErrors).Will(Return.Value(true));
			Expect.Once.On(_logger).Method(LogError).WithAnyArguments();

			GoogleTestsRunner runner = new GoogleTestsRunner(_logger, false, "bad");
			ExecutionResult result = runner.Import();

			Assert.That(result.Status, Is.False);
			Assert.That(result.Message, Is.Null);
		}
	}
}