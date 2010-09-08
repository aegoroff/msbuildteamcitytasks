/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using ILogger = MSBuild.TeamCity.Tasks.ILogger;
using Is = NUnit.Framework.Is;

namespace Tests
{
	[TestFixture]
	public class TTeamCityTaskImplementation
	{
		private Mockery _mockery;
		private ILogger _logger;
		private TeamCityMessage _message;
		private TeamCityTaskImplementation _implementation;

		private const string LogMessage = "LogMessage";
		private const string BuildNumber = "buildNumber";
		private const string TeamCityEnvVar = "TEAMCITY_PROJECT_NAME";
		private const string TeamCityProject = "prj";

		[SetUp]
		public void Setup()
		{
			_mockery = new Mockery();
			_logger = _mockery.NewMock<ILogger>();

			const string number = "1.0";
			_message = new SimpleTeamCityMessage(BuildNumber, number);
			_implementation = new TeamCityTaskImplementation(_logger);
		}

		[Test]
		public void WriteMessageOutsideTeamCityEnvironment()
		{
			Expect.Never.On(_logger).Method(LogMessage).WithAnyArguments();
			_implementation.Write(_message);
		}

		[Test]
		public void WriteMessageInsideTeamCityEnvironment()
		{
			Expect.Once.On(_logger).Method(LogMessage).WithAnyArguments();

			Environment.SetEnvironmentVariable(TeamCityEnvVar, TeamCityProject, EnvironmentVariableTarget.Process);

			try
			{
				_implementation.Write(_message);
			}
			finally
			{
				Environment.SetEnvironmentVariable(TeamCityEnvVar, null, EnvironmentVariableTarget.Process);
			}
		}
		
		[Test]
		[ExpectedException(typeof(Exception))]
		public void WriteMessageInsideTeamCityEnvironmentThrowException()
		{
			Expect.Once.On(_logger).Method(LogMessage).Will(Throw.Exception(new Exception()));

			Environment.SetEnvironmentVariable(TeamCityEnvVar, TeamCityProject, EnvironmentVariableTarget.Process);

			try
			{
				_implementation.Write(_message);
			}
			finally
			{
				Environment.SetEnvironmentVariable(TeamCityEnvVar, null, EnvironmentVariableTarget.Process);
			}
		}

		[Test]
		public void ExecuteTrue()
		{
			Expect.Once.On(_logger).Method(LogMessage).WithAnyArguments();

			ExecutionResult result = new ExecutionResult { Messages = new List<TeamCityMessage>(), Status = true };
			result.Messages.Add(_message);
			Assert.That(_implementation.Execute(result));
		}
		
		[Test]
		public void ExecuteFalse()
		{
			Expect.Once.On(_logger).Method(LogMessage).WithAnyArguments();
			
			ExecutionResult result = new ExecutionResult { Messages = new List<TeamCityMessage>(), Status = false };
			result.Messages.Add(_message);
			Assert.That(_implementation.Execute(result), Is.False);
		}
		
		[Test]
		public void ExecuteNoMessage()
		{
			Expect.Never.On(_logger).Method(LogMessage).WithAnyArguments();
			
			ExecutionResult result = new ExecutionResult { Status = true };
			Assert.That(_implementation.Execute(result), Is.True);
		}

		[Test]
		public void ExecuteFull()
		{
			const string flowId = "1";
			Expect.Once.On(_logger).Method(LogMessage).WithAnyArguments();

			ExecutionResult result = new ExecutionResult { Messages = new List<TeamCityMessage>(), Status = true };
			result.Messages.Add(_message);
			
			Assert.That(_implementation.Execute(result, true, flowId));
			Assert.That(_message.IsAddTimestamp);
			Assert.That(_message.FlowId, Is.EqualTo(flowId));
		}
	}
}