/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;
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
		/// Gets or sets message text
		/// </summary>
		[Required]
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets message status. 
		/// The status attribute may take following values: NORMAL, WARNING, FAILURE, ERROR. The default value is NORMAL.
		/// </summary>
		public string Status { get; set; }

		///<summary>
		/// Gets or sets error details text that is used only if status is ERROR, in other cases it is ignored.
		///</summary>
		public string ErrorDetails { get; set; }

		/// <summary>
		/// Reads TeamCity messages
		/// </summary>
		/// <returns>TeamCity messages list</returns>
		protected override IEnumerable<TeamCityMessage> ReadMessages()
		{
			yield return new ReportMessageBuilder(Text, Status, ErrorDetails).BuildMessage();
		}
	}
}