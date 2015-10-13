/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Represents common statistic reporter into TeamCity
    /// </summary>
    /// <example>
    ///     Reports statistic into TeamCity.
    ///     <code><![CDATA[
    /// <ReportBuildStatistic
    ///     Key="Param1"
    ///     Value="12.1"
    /// />
    /// ]]></code>
    ///     Reports statistic into TeamCity full example (with all optional attributes).
    ///     <code><![CDATA[
    /// <ReportBuildStatistic
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Key="Param1"
    ///     Value="12.1"
    /// />
    /// ]]></code>
    /// </example>
    public class ReportBuildStatistic : TeamCityTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportBuildStatistic" /> class
        /// </summary>
        public ReportBuildStatistic()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportBuildStatistic" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public ReportBuildStatistic(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Gets or sets statistic parameter name
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets statistic parameter value
        /// </summary>
        [Required]
        public float Value { get; set; }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new BuildStatisticTeamCityMessage(this.Key, this.Value);
        }
    }
}