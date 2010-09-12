/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Sets desired build number of a build configuration
    /// </summary>
    /// <example>Sets current build number
    /// <code><![CDATA[
    /// <BuildNumber Number="20.3" />
    /// ]]></code>
    /// Sets current build number full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BuildNumber
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Number="20.3"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildNumber : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="BuildNumber"/> class
        ///</summary>
        public BuildNumber()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="BuildNumber"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BuildNumber( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets build number value
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new SimpleTeamCityMessage("buildNumber", Number);
        }
    }
}