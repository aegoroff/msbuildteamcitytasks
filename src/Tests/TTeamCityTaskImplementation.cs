/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;
using NMock2;
using NUnit.Framework;
using Tests.Utils;
using ILogger = MSBuild.TeamCity.Tasks.ILogger;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TTeamCityTaskImplementation
    {
        private Mockery mockery;
        private ILogger logger;
        private TeamCityMessage message;
        private TeamCityTaskImplementation implementation;

        internal const string LogMessage = "LogMessage";
        internal const string LogError = "LogErrorFromException";
        private const string BuildNumber = "buildNumber";

        [SetUp]
        public void Setup()
        {
            mockery = new Mockery();
            logger = mockery.NewMock<ILogger>();

            const string number = "1.0";
            message = new SimpleTeamCityMessage(BuildNumber, number);
            implementation = new TeamCityTaskImplementation(logger);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WriteNullMessage()
        {
            implementation.Write(null);
        }

        [Test]
        public void WriteMessageOutsideTeamCityEnvironment()
        {
            Expect.Never.On(logger).Method(LogMessage).WithAnyArguments();
            implementation.Write(message);
        }

        [Test]
        public void WriteMessageInsideTeamCityEnvironment()
        {
            Expect.Once.On(logger).Method(LogMessage).WithAnyArguments();
            using (new TeamCityEnv())
            {
                implementation.Write(message);
            }
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void WriteMessageInsideTeamCityEnvironmentThrowException()
        {
            Expect.Once.On(logger).Method(LogMessage).Will(Throw.Exception(new Exception()));
            using (new TeamCityEnv())
            {
                implementation.Write(message);
            }
        }

        [Test]
        public void ExecuteTrue()
        {
            Expect.Once.On(logger).Method(LogMessage).WithAnyArguments();

            ExecutionResult result = new ExecutionResult { Messages = new List<TeamCityMessage>(), Status = true };
            result.Messages.Add(message);
            Assert.That(implementation.Execute(result));
        }

        [Test]
        public void ExecuteFalse()
        {
            Expect.Once.On(logger).Method(LogMessage).WithAnyArguments();

            ExecutionResult result = new ExecutionResult { Messages = new List<TeamCityMessage>(), Status = false };
            result.Messages.Add(message);
            Assert.That(implementation.Execute(result), Is.False);
        }

        [Test]
        public void ExecuteNoMessage()
        {
            Expect.Never.On(logger).Method(LogMessage).WithAnyArguments();

            ExecutionResult result = new ExecutionResult { Status = true };
            Assert.That(implementation.Execute(result), Is.True);
        }

        [Test]
        public void ExecuteFull()
        {
            const string flowId = "1";
            Expect.Once.On(logger).Method(LogMessage).WithAnyArguments();

            ExecutionResult result = new ExecutionResult { Messages = new List<TeamCityMessage>(), Status = true };
            result.Messages.Add(message);

            Assert.That(implementation.Execute(result, true, flowId));
            Assert.That(message.IsAddTimestamp);
            Assert.That(message.FlowId, Is.EqualTo(flowId));
        }
    }
}