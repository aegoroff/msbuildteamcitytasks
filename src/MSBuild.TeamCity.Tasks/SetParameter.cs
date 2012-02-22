/*
 * Created by: egr
 * Created at: 22.02.2012
 * © 2007-2012 Alexander Egorov
 */

using System.Collections.Generic;
using MSBuild.TeamCity.Tasks.Messages;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Adding or Changing a Build Parameter from a Build Step.
    /// By using dedicated service message in your build script, 
    /// you can dynamically update some build parameters right from a build step, 
    /// so that following build steps will run with modified set of build parameters.
    /// </summary>
    /// <example>Sets build parameter "n1"
    /// <code><![CDATA[
    /// <SetParameter 
    ///     Name="n1" 
    ///     Value="value" 
    /// />
    /// ]]></code>
    /// Sets build parameter "n1" full example (with all optional attributes)
    /// <code><![CDATA[
    /// <SetParameter
    ///    IsAddTimestamp="true"
    ///    FlowId="1"
    ///    Name="n1"
    ///	   Value="v1"
    /// />
    /// ]]></code>
    /// </example>
    public class SetParameter : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="SetParameter"/> class
        ///</summary>
        public SetParameter()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="SetParameter"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public SetParameter(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets parameter name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets parameter value
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new SetParameterTeamCityMessage(Name, Value);
        }
    }
}