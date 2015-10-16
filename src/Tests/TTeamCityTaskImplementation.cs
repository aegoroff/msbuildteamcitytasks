/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;
using NMock;
using Tests.Utils;
using Xunit;
using ILogger = MSBuild.TeamCity.Tasks.ILogger;

namespace Tests
{
    [Collection("SerialTests")]
    public class TTeamCityTaskImplementation
    {
        private const string BuildNumber = "buildNumber";
        private readonly TeamCityTaskImplementation implementation;
        private readonly Mock<ILogger> logger;
        private readonly TeamCityMessage message;

        public TTeamCityTaskImplementation()
        {
            var mockery = new MockFactory();
            this.logger = mockery.CreateMock<ILogger>();

            const string number = "1.0";
            this.message = new SimpleTeamCityMessage(BuildNumber, number);
            this.implementation = new TeamCityTaskImplementation(this.logger.MockObject);
        }

        [Fact]
        public void WriteNullMessage()
        {
            Assert.Throws<ArgumentNullException>(
                delegate { implementation.Write(null); });
        }

        [Fact]
        public void WriteMessageOutsideTeamCityEnvironment()
        {
            this.logger.Expects.No.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            this.implementation.Write(this.message);
        }

        [Fact]
        public void WriteMessageInsideTeamCityEnvironment()
        {
            this.logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();
            using (new TeamCityEnv())
            {
                this.implementation.Write(this.message);
            }
        }

        [Fact]
        public void WriteMessageInsideTeamCityEnvironmentThrowException()
        {
            Assert.Throws<Exception>(
                delegate
                {
                    logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null))
                        .WithAnyArguments()
                        .Will(Throw.Exception(new Exception()));
                    using (new TeamCityEnv())
                    {
                        implementation.Write(message);
                    }
                });
        }

        [Fact]
        public void ExecuteTrue()
        {
            this.logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            var result = new ExecutionResult(true);
            result.Messages.Add(this.message);
            this.implementation.Execute(result).Should().BeTrue();
        }

        [Fact]
        public void ExecuteFalse()
        {
            this.logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            var result = new ExecutionResult(false);
            result.Messages.Add(this.message);
            this.implementation.Execute(result).Should().BeFalse();
        }

        [Fact]
        public void ExecuteNoMessage()
        {
            this.logger.Expects.No.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            var result = new ExecutionResult(true);
            this.implementation.Execute(result).Should().BeTrue();
        }

        [Fact]
        public void ExecuteFull()
        {
            const string flowId = "1";
            this.logger.Expects.One.Method(_ => _.LogMessage(MessageImportance.High, null)).WithAnyArguments();

            var result = new ExecutionResult(true);
            result.Messages.Add(this.message);

            this.implementation.Execute(result, true, flowId).Should().BeTrue();
            this.message.IsAddTimestamp.Should().BeTrue();
            this.message.FlowId.Should().Be(flowId);
        }

        [Fact]
        public void ExecuteNullResult()
        {
            Assert.Throws<ArgumentNullException>(
                delegate { implementation.Execute(null).Should().BeTrue(); });
        }
    }
}