/*
 * Created by: egr
 * Created at: 09.09.2010
 * © 2007-2012 Alexander Egorov
 */

using System;
using MSBuild.TeamCity.Tasks;
using NMock2;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
    public class TTask
    {
        protected ILogger Logger { get; private set; }

        protected Mockery Mockery { get; private set; }

        [SetUp]
        public void Setup()
        {
            Mockery = new Mockery();
            Logger = Mockery.NewMock<ILogger>();
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar,
                                               TeamCityEnv.TeamCityProject,
                                               EnvironmentVariableTarget.Process);
        }

        [TearDown]
        public void Teardown()
        {
            Environment.SetEnvironmentVariable(TeamCityEnv.TeamCityEnvVar, null,
                                               EnvironmentVariableTarget.Process);
        }
    }
}