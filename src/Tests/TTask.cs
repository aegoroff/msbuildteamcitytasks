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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TTask()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar, null,
                    EnvironmentVariableTarget.Process);
            }
        }
    }
}