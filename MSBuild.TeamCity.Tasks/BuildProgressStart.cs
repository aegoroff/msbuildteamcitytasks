/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Writes progress start message into TeamCity log
	/// </summary>
	/// <example>Writes progress start message into TeamCity log
	/// <code><![CDATA[
	/// <BuildProgressStart Message="Message text" />
	/// ]]></code>
	/// Writes progress start message into TeamCity log full example (with all optional attributes)
	/// <code><![CDATA[
	/// <BuildProgressStart
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Message="Message text"
	/// />
	/// ]]></code>
	/// </example>
	public class BuildProgressStart : TeamCityTask
	{
		/// <summary>
		/// Gets o sets progress message text
		/// </summary>
		[Required]
		public string Message { get; set; }
		
		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new SimpleTeamCityMessage("progressStart", Message));
			return true;
		}
	}
}