/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using FluentAssertions;
using Microsoft.Build.Framework;
using Moq;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;
using Tests.Utils;
using Xunit;
using ILogger = MSBuild.TeamCity.Tasks.ILogger;

namespace Tests
{

    public class TTeamCityTaskImplementation
    {
        private const string BuildNumber = "buildNumber";
        private readonly TeamCityTaskImplementation implementation;
        private readonly Mock<ILogger> logger;
        private readonly TeamCityMessage message;

        public TTeamCityTaskImplementation()
        {
            this.logger = new Mock<ILogger>();

            const string number = "1.0";
            this.message = new SimpleTeamCityMessage(BuildNumber, number);
            this.implementation = new TeamCityTaskImplementation(this.logger.Object);
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
            this.logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 1
            this.implementation.Write(this.message);
            this.logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMostOnce);
        }

        [Fact]
        public void WriteMessageInsideTeamCityEnvironment()
        {
            this.logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())); // 1
            using (new TeamCityEnv())
            {
                this.implementation.Write(this.message);
            }
            this.logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void WriteMessageInsideTeamCityEnvironmentThrowException()
        {
            Assert.Throws<Exception>(
                delegate
                {
                    logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>())).Throws<Exception>();
                    using (new TeamCityEnv())
                    {
                        implementation.Write(message);
                    }
                    logger.Verify();
                });
        }

        [Fact]
        public void ExecuteTrue()
        {
            this.logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));

            var result = new ExecutionResult(true);
            result.Messages.Add(this.message);
            this.implementation.Execute(result).Should().BeTrue();

            this.logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMostOnce);
        }

        [Fact]
        public void ExecuteFalse()
        {
            this.logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));

            var result = new ExecutionResult(false);
            result.Messages.Add(this.message);
            this.implementation.Execute(result).Should().BeFalse();

            this.logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMostOnce);
        }

        [Fact]
        public void ExecuteNoMessage()
        {
            this.logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));

            var result = new ExecutionResult(true);
            this.implementation.Execute(result).Should().BeTrue();

            this.logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void ExecuteFull()
        {
            const string flowId = "1";
            this.logger.Setup(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()));

            var result = new ExecutionResult(true);
            result.Messages.Add(this.message);

            this.implementation.Execute(result, true, flowId).Should().BeTrue();
            this.message.IsAddTimestamp.Should().BeTrue();
            this.message.FlowId.Should().Be(flowId);

            this.logger.Verify(_ => _.LogMessage(MessageImportance.High, It.IsAny<string>()), Times.AtMostOnce);
        }

        [Fact]
        public void ExecuteNullResult()
        {
            Assert.Throws<ArgumentNullException>(
                delegate { implementation.Execute(null).Should().BeTrue(); });
        }
    }
}