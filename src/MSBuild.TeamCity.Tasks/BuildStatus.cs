/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// TeamCity allows user to change the build status directly from the build script. 
	/// You can also permanently change the build status text for your build
	/// </summary>
	/// <example>Changes (sets) the status of a build
	/// <code><![CDATA[
	/// <BuildStatus
	///		Status="SUCCESS"
	///		Text="The app build succeed" 
	/// />
	/// ]]></code>
	/// Changes (sets) the status of a build full example (with all optional attributes)
	/// <code><![CDATA[
	/// <BuildStatus
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Status="SUCCESS"
	///		Text="The app build succeed" 
	/// />
	/// ]]></code>
	/// </example>
	public class BuildStatus : TeamCityTask
	{
		///<summary>
		/// Initializes a new instance of the <see cref="BuildStatus"/> class
		///</summary>
		public BuildStatus()
		{
		}

		///<summary>
		/// Initializes a new instance of the <see cref="BuildStatus"/> class using 
		/// logger specified
		///</summary>
		///<param name="logger"><see cref="ILogger"/> implementation</param>
		public BuildStatus( ILogger logger )
			: base(logger)
		{
		}

		/// <summary>
		/// Gets or sets the status attribute that may take following values: FAILURE, SUCCESS.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets build status text
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Reads TeamCity messages
		/// </summary>
		/// <returns>TeamCity messages list</returns>
		protected override IEnumerable<TeamCityMessage> ReadMessages()
		{
			yield return new BuildStatusTeamCityMessage(Status, Text);
		}
	}
}