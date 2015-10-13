/*
 * Created by: egr
 * Created at: 27.01.2011
 * © 2007-2013 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents test output reporting TC message
    /// </summary>
    public class TestStdOutTeamCityMessage : NamedTeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TestStdOutTeamCityMessage" /> class
        /// </summary>
        /// <param name="name">Test's name</param>
        /// <param name="output">Test output</param>
        public TestStdOutTeamCityMessage(string name, string output) : base(name)
        {
            this.Attributes.Add("out", output);
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "testStdOut"; }
        }
    }
}