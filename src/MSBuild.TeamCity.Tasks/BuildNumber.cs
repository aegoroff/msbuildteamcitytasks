/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

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
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Number="20.3"
	/// />
	/// ]]></code>
	/// </example>
	public class BuildNumber : TeamCityTask
	{
		/// <summary>
		/// Build number value
		/// </summary>
		[Required]
		public string Number { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new SimpleTeamCityMessage("buildNumber", Number));
			return true;
		}
	}
}