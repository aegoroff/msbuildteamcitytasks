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
		/// Gets or sets block name
		/// </summary>
		[Required]
		public string Name { get; set; }

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
		protected abstract NamedTeamCityMessage CreateMessage();
	}
}