/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

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
		/// <summary>
		/// Gets or sets the status attribute that may take following values: FAILURE, SUCCESS.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets build status text
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new BuildStatusTeamCityMessage(Status, Text));
			return true;
		}
	}
}