/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Reports messages for build log
	/// </summary>
	/// <example>Report message using NORMAL status (default)
	/// <code><![CDATA[
	/// <ReportMessage Text="Message text" />
	/// ]]></code>
	/// Report message using status specified
	/// <code><![CDATA[
	/// <ReportMessage
	///		Status="WARNING"
	///		Text="Message text"
	/// />
	/// ]]></code>
	/// Report message using status and error details specified (only ERROR mode)
	/// <code><![CDATA[
	/// <ReportMessage
	///		Status="ERROR"
	///		ErrorDetails="Error details"
	///		Text="Message text"
	/// />
	/// ]]></code>
	/// Report message full example (with all optional attributes)
	/// <code><![CDATA[
	/// <ReportMessage
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Status="ERROR"
	///		ErrorDetails="Error details"
	///		Text="Message text"
	/// />
	/// ]]></code>
	/// </example>
	public class ReportMessage : TeamCityTask
	{
		/// <summary>
		/// Message text
		/// </summary>
		[Required]
		public string Text { get; set; }

		/// <summary>
		/// Message status. The status attribute may take following values: NORMAL, WARNING, FAILURE, ERROR. The default value is NORMAL.
		/// </summary>
		public string Status { get; set; }

		///<summary>
		/// ErrorDetails is used only if status is ERROR, in other cases it is ignored.
		///</summary>
		public string ErrorDetails { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			ReportMessageTeamCityMessage message;

			if ( !string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(ErrorDetails) )
			{
				message = new ReportMessageTeamCityMessage(Text, Status, ErrorDetails);
			}
			else if ( !string.IsNullOrEmpty(Status) )
			{
				message = new ReportMessageTeamCityMessage(Text, Status);
			}
			else
			{
				message = new ReportMessageTeamCityMessage(Text);
			}

			Write(message);
			return true;
		}
	}
}