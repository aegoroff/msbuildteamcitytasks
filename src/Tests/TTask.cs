/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using NMock;
using Tests.Utils;

namespace Tests
{
    public class TTask : IDisposable
    {
        protected TTask()
        {
            this.Mockery = new MockFactory();
            this.Logger = this.Mockery.CreateMock<ILogger>();
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar,
                TeamCityEnv.TeamCityProject,
                EnvironmentVariableTarget.Process);
        }

        protected Mock<ILogger> Logger { get; private set; }

        protected MockFactory Mockery { get; }

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