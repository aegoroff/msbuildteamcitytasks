/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	public class BuildNumberTeamCityMessage : TeamCityMessage
	{
		public string BuildNumber { get; private set; }

		public BuildNumberTeamCityMessage(string buildNumber)
		{
			BuildNumber = buildNumber;
			Message = string.Format(CultureInfo.InvariantCulture, "buildNumber '{0}'", BuildNumber);
		}
	}
}