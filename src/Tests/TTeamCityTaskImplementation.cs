/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;
using Microsoft.Build.Framework;
using NMock;
using NUnit.Framework;
using Tests.Utils;
using ILogger = MSBuild.TeamCity.Tasks.ILogger;
using Is = NUnit.Framework.Is;

namespace Tests
{
    [TestFixture]
    public class TTeamCityTaskImplementation
    {
        private MockFactory mockery;
        private Mock<ILogger> logger;
        private TeamCityMessage message;
        private TeamCityTaskImplementation implementation;

        private const string BuildNumber = "buildNumber";

        [SetUp]
        public void Setup()
        {
            mockery = new MockFactory();
            logger = mockery.CreateMock<ILogger>();

            const string number = "1.0";
            message = new SimpleTeamCityMessage(BuildNumber, number);
            implementation = new TeamCityTaskImplementation(logger.MockObject);
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
            logger.Expects.No.Method(_=> _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            implementation.Write(message);
        }

        [Test]
        public void WriteMessageInsideTeamCityEnvironment()
        {
            logger.Expects.One.Method(_=> _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            using (new TeamCityEnv())
            {
                implementation.Write(message);
            }
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void WriteMessageInsideTeamCityEnvironmentThrowException()
        {
            logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments().Will(Throw.Exception(new Exception()));
            using (new TeamCityEnv())
            {
                implementation.Write(message);
            }
        }

        [Test]
        public void ExecuteTrue()
        {
            logger.Expects.One.Method(_=> _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ExecutionResult result = new ExecutionResult(true);
            result.Messages.Add(message);
            Assert.That(implementation.Execute(result));
        }

        [Test]
        public void ExecuteFalse()
        {
            logger.Expects.One.Method(_=> _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ExecutionResult result = new ExecutionResult(false);
            result.Messages.Add(message);
            Assert.That(implementation.Execute(result), Is.False);
        }

        [Test]
        public void ExecuteNoMessage()
        {
            logger.Expects.No.Method(_=> _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ExecutionResult result = new ExecutionResult(true);
            Assert.That(implementation.Execute(result), Is.True);
        }

        [Test]
        public void ExecuteFull()
        {
            const string flowId = "1";
            logger.Expects.One.Method(_=> _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            ExecutionResult result = new ExecutionResult(true);
            result.Messages.Add(message);

            Assert.That(implementation.Execute(result, true, flowId));
            Assert.That(message.IsAddTimestamp);
            Assert.That(message.FlowId, Is.EqualTo(flowId));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteNullResult()
        {
            Assert.That(implementation.Execute(null));
        }
    }
}