/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represent TC build number message
	/// </summary>
	public class BuildNumberTeamCityMessage : TeamCityMessage
	{
		/// <summary>
		/// Gets build number to set
		/// </summary>
		public string BuildNumber { get; private set; }

		/// <summary>
		/// Creates new message instance using build number specified
		/// </summary>
		/// <param name="buildNumber">Number (string) to set as build number</param>
		public BuildNumberTeamCityMessage(string buildNumber)
		{
			BuildNumber = buildNumber;
			Message = string.Format(CultureInfo.InvariantCulture, "buildNumber '{0}'", Escape(BuildNumber));
		}
	}
}