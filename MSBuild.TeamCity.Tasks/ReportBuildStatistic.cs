/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents common statistic reporter into TeamCity
	/// </summary>
	/// <example>Reports statistic into TeamCity.
	/// <code><![CDATA[
	/// <ReportBuildStatistic
	///		Key="Param1"
	///		Value="12.1"
	/// />
	/// ]]></code>
	/// Reports statistic into TeamCity full example (with all optional attributes).
	/// <code><![CDATA[
	/// <ReportBuildStatistic
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Key="Param1"
	///		Value="12.1"
	/// />
	/// ]]></code>
	/// </example>
	public class ReportBuildStatistic : TeamCityTask
	{
		/// <summary>
		/// Gets or set statistic parameter name
		/// </summary>
		[Required]
		public string Key { get; set; }

		/// <summary>
		/// Gets or set statistic parameter value
		/// </summary>
		[Required]
		public float Value { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			var message = new BuildStatisticTeamCityMessage(Key, Value);
			Write(message);
			return true;
		}
	}
}