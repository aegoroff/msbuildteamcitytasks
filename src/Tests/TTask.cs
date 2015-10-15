/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using NMock;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
    public class TTask
    {
        protected Mock<ILogger> Logger { get; private set; }

        protected MockFactory Mockery { get; private set; }

        [SetUp]
        public void Setup()
        {
            Mockery = new MockFactory();
            Logger = Mockery.CreateMock<ILogger>();
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar,
                                               TeamCityEnv.TeamCityProject,
                                               EnvironmentVariableTarget.Process);
            AfterSetup();
        }

        protected virtual void AfterSetup()
        {
        }

        [TearDown]
        public void Teardown()
        {
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar, null,
                                               EnvironmentVariableTarget.Process);
            AfterTeardown();
        }

        protected virtual void AfterTeardown()
        {
        }
    }
}