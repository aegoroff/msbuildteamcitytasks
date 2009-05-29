/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents TC build statistic message
	/// </summary>
	public class BuildStatisticTeamCityMessage : TeamCityMessage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BuildStatisticTeamCityMessage"/> class
		/// </summary>
		/// <param name="key">Parameter key</param>
		/// <param name="value">Parameter value</param>
		public BuildStatisticTeamCityMessage( string key, float value )
		{
			Key = key;
			Value = value;
			Attributes.Add(new MessageAttributeItem("key", Key));
			Attributes.Add(new MessageAttributeItem("value", string.Format(CultureInfo.InvariantCulture, "{0:F}", Value)));
		}

		/// <summary>
		/// Gets parameter key
		/// </summary>
		public string Key { get; private set; }

		/// <summary>
		/// Gets parameter value
		/// </summary>
		public float Value { get; private set; }

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "buildStatisticValue"; }
		}
	}
}