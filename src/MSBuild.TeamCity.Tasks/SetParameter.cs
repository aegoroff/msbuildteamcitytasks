/*
 * Created by: egr
 * Created at: 22.02.2012
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Adding or Changing a Build Parameter from a Build Step.
    /// By using dedicated service message in your build script, 
    /// you can dynamically update some build parameters right from a build step, 
    /// so that following build steps will run with modified set of build parameters.
    /// </summary>
    /// <remarks>
    /// When specifying a build parameter's name, mind the prefix:
    /// <ul>
    /// <li><b>system</b> for system properties.</li>
    /// <li><b>env</b> for environment variables. </li>
    /// <li>no prefix for configuration parameter.</li>
    /// </ul>
    /// <p/>
    /// <a href="http://confluence.jetbrains.net/display/TCD7/Configuring+Build+Parameters">Read more about build parameters and their prefixes</a> 
    /// The changed build parameters will also be available in dependent builds as %dep.% properties.
    /// </remarks>
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
    ///    Value="v1"
    /// />
    /// ]]></code>
    /// Sets environment variable VAR1
    /// <code><![CDATA[
    /// <SetParameter 
    ///     Name="env.VAR1" 
    ///     Value="value" 
    /// />
    /// ]]></code>
    /// Sets system property prop1
    /// <code><![CDATA[
    /// <SetParameter 
    ///     Name="system.prop1" 
    ///     Value="value" 
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