/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Writes progress message into TeamCity log
	/// </summary>
	/// <example>Writes progress message into TeamCity log
	/// <code><![CDATA[
	/// <BuildProgressMessage Message="Message text" />
	/// ]]></code>
	/// Writes progress message into TeamCity log full example (with all optional attributes)
	/// <code><![CDATA[
	/// <BuildProgressMessage
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Message="Message text"
	/// />
	/// ]]></code>
	/// </example>
	public class BuildProgressMessage : BuildProgressTask
	{
		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new SimpleTeamCityMessage("progressMessage", Message));
			return true;
		}
	}
}