/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represent base class of all BuildProgress* tasks. Cannot be used directly (because it's abstract) in MSBuild script.
	///</summary>
	public abstract class BuildProgressTask : TeamCityTask
	{
		/// <summary>
		/// Gets or sets progress message text
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
			Write(CreateMessage());
			return true;
		}

		/// <summary>
		/// Creates concrete message class
		/// </summary>
		/// <returns>New message instance</returns>
		protected abstract SimpleTeamCityMessage CreateMessage();
	}
}