/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using Moq;
using MSBuild.TeamCity.Tasks;
using Tests.Utils;

namespace Tests
{
    public class TTask : IDisposable
    {
        protected TTask()
        {
            this.Logger = new Mock<ILogger>();
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar,
                TeamCityEnv.TeamCityProject,
                EnvironmentVariableTarget.Process);
        }

        protected Mock<ILogger> Logger { get; private set; }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar, null,
                EnvironmentVariableTarget.Process);
            this.AfterTeardown();
        }

        protected virtual void AfterTeardown()
        {
        }
    }
}