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
		/// Gets o sets progress message text
		/// </summary>
		[Required]
		public string Message { get; set; }
	}
}