/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2015 Alexander Egorov
 */

using System.Diagnostics;
using System.Globalization;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents TC build statistic message
    /// </summary>
    public class BuildStatisticTeamCityMessage : TeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BuildStatisticTeamCityMessage" /> class
        /// </summary>
        /// <param name="key">Parameter key</param>
        /// <param name="value">Parameter value</param>
        public BuildStatisticTeamCityMessage(string key, float value)
        {
            this.Key = key;
            this.Value = value;
            this.Attributes.Add("key", this.Key);
            this.Attributes.Add("value", string.Format(CultureInfo.InvariantCulture, "{0:F}", this.Value));
        }

        /// <summary>
        ///     Gets parameter key
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Gets parameter value
        /// </summary>
        public float Value { get; }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "buildStatisticValue"; }
        }
    }
}