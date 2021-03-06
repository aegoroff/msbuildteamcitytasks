/*
 * Created by: egr
 * Created at: 09.05.2009
 * � 2007-2015 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents test suite start TC message
    /// </summary>
    public class TestSuiteStartTeamCityMessage : NamedTeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TestSuiteStartTeamCityMessage" /> class using name specified
        /// </summary>
        /// <param name="name">Name attribute value</param>
        public TestSuiteStartTeamCityMessage(string name) : base(name)
        {
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "testSuiteStarted"; }
        }
    }
}