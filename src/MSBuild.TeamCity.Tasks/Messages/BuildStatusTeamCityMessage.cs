/*
 * Created by: egr
 * Created at: 03.05.2009
 * � 2007-2015 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents TeamCity build status setting message.
    /// </summary>
    public class BuildStatusTeamCityMessage : TeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BuildStatusTeamCityMessage" /> class using status and text specified.
        /// </summary>
        /// <param name="status">The status attribute may take following values: FAILURE, SUCCESS.</param>
        /// <param name="text">Some useful status text</param>
        public BuildStatusTeamCityMessage(string status, string text)
        {
            this.Attributes.Add("status", status);
            this.Attributes.Add("text", text);
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "buildStatus"; }
        }
    }
}