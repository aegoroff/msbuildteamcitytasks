/*
 * Created by: egr
 * Created at: 04.12.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents all possible dot net coverage tools
	/// </summary>
	public enum DotNetCoverageTool
	{
		/// <summary>
		/// PartCover tool
		/// </summary>
		PartCover,

		/// <summary>
		/// NCover 1.5.x tool
		/// </summary>
		Ncover,

		/// <summary>
		/// NCover 3.x tool
		/// </summary>
		Ncover3
	}
}