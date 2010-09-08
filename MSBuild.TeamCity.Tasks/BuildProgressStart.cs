/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

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
	public class BuildProgressStart : BuildProgressTask
	{
		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string MessageName
		{
			get { return "progressStart"; }
		}
	}
}