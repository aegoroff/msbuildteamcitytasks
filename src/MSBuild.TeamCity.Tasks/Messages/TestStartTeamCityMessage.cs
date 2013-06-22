/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2012 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents test start TC message
    ///</summary>
    public class TestStartTeamCityMessage : NamedTeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestStartTeamCityMessage"/> class using name specified
        ///</summary>
        ///<param name="name">Name attribute value</param>
        ///<param name="captureStandardOutput">Whether to capture standart output</param>
        public TestStartTeamCityMessage(string name, bool captureStandardOutput)
            : base(name)
        {
            if (captureStandardOutput)
            {
                Attributes.Add("captureStandardOutput", "true");
            }
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough]
            get { return "testStarted"; }
        }
    }
}