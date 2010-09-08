/*
 * Created by: egr
 * Created at: 08.09.2010
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
	public class TNCoverReport
	{
		private Mockery _mockery;
		private ILogger _logger;

		[SetUp]
		public void Setup()
		{
			_mockery = new Mockery();
			_logger = _mockery.NewMock<ILogger>();
			Environment.SetEnvironmentVariable(TTeamCityTaskImplementation.TeamCityEnvVar,
			                                   TTeamCityTaskImplementation.TeamCityProject, EnvironmentVariableTarget.Process);
		}

		[TearDown]
		public void Teardown()
		{
			Environment.SetEnvironmentVariable(TTeamCityTaskImplementation.TeamCityEnvVar, null,
			                                   EnvironmentVariableTarget.Process);
		}

		[Test]
		public void OnlyRequired()
		{
			Expect.Exactly(2).On(_logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			NCoverReport task = new NCoverReport(_logger)
			                    	{
			                    		NCoverExplorerPath = "p",
			                    		XmlReportPath = "path"
			                    	};
			Assert.That(task.Execute());
		}

		[Test]
		public void OnlyRequiredAndArguments()
		{
			Expect.Exactly(3).On(_logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			NCoverReport task = new NCoverReport(_logger)
			                    	{
			                    		NCoverExplorerPath = "p",
			                    		XmlReportPath = "path",
			                    		Arguments = "a"
			                    	};
			Assert.That(task.Execute());
		}

		[Test]
		public void OnlyRequiredAndArgumentsAndReportType()
		{
			Expect.Exactly(4).On(_logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			NCoverReport task = new NCoverReport(_logger)
			                    	{
			                    		NCoverExplorerPath = "p",
			                    		XmlReportPath = "path",
			                    		Arguments = "a",
			                    		ReportType = "1"
			                    	};
			Assert.That(task.Execute());
		}

		[Test]
		public void Full()
		{
			Expect.Exactly(5).On(_logger).Method(TTeamCityTaskImplementation.LogMessage).WithAnyArguments();

			NCoverReport task = new NCoverReport(_logger)
			                    	{
			                    		NCoverExplorerPath = "p",
			                    		XmlReportPath = "path",
			                    		Arguments = "a",
			                    		ReportType = "1",
			                    		ReportOrder = "1"
			                    	};
			Assert.That(task.Execute());
		}
		
		[Test]
		public void WithException()
		{
			Expect.Once.On(_logger).Method(TTeamCityTaskImplementation.LogMessage).Will(Throw.Exception(new Exception()));

			NCoverReport task = new NCoverReport(_logger)
			                    	{
			                    		NCoverExplorerPath = "p",
			                    		XmlReportPath = "path",
			                    	};
			Assert.That(task.Execute(), Is.False);
		}
	}
}