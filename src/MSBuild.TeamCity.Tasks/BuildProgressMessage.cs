/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

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
		/// Creates concrete message class
		/// </summary>
		/// <returns>New message instance</returns>
		protected override SimpleTeamCityMessage CreateMessage()
		{
			return new SimpleTeamCityMessage("progressMessage", Message);
		}
	}
}