/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represent base class of all Block* tasks. Cannot be used directly (because it's abstract) in MSBuild script.
	///</summary>
	public abstract class BlockTask : TeamCityTask
	{
		/// <summary>
		/// Block name
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Creates concrete message class
		/// </summary>
		/// <returns></returns>
		protected abstract BlockTeamCityMessage CreateMessage();

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			BlockTeamCityMessage message = CreateMessage();
			message.IsAddTimeStamp = IsAddTimestamp;
			message.FlowId = FlowId;
			Log.LogMessage(MessageImportance.High, message.ToString());
			return true;
		}
	}
}