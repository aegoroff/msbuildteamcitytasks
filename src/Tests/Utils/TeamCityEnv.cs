/*
 * Created by: egr
 * Created at: 18.10.2010
 * © 2007-2011 Alexander Egorov
 */

using System;

namespace Tests.Utils
{
    internal sealed class TeamCityEnv : IDisposable
    {
        internal const string TeamCityEnvVar = "TEAMCITY_PROJECT_NAME";
        internal const string TeamCityProject = "prj";

        internal TeamCityEnv()
        {
            Environment.SetEnvironmentVariable(TeamCityEnvVar, TeamCityProject, EnvironmentVariableTarget.Process);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private static void Dispose(bool disposing)
        {
            if ( disposing )
            {
                Environment.SetEnvironmentVariable(TeamCityEnvVar, null, EnvironmentVariableTarget.Process);
            }
        }
    }
}