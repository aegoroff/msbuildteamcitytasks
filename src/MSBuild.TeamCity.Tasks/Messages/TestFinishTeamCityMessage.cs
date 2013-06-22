/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System.Diagnostics;
using System.Globalization;

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents test finish TC message
    ///</summary>
    public class TestFinishTeamCityMessage : NamedTeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="TestFinishTeamCityMessage"/> class using name and duration in seconds specified
        ///</summary>
        ///<param name="name">Name attribute value</param>
        ///<param name="durationSeconds">Test duration in seconds</param>
        public TestFinishTeamCityMessage(string name, double durationSeconds) : base(name)
        {
            var duration = durationSeconds * 1000;
            Attributes.Add("duration", duration.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough]
            get { return "testFinished"; }
        }
    }
}