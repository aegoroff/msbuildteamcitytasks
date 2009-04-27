/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	public class PublishArtifactTeamCityMessage : TeamCityMessage
	{
		public string Path { get; set; }

		public PublishArtifactTeamCityMessage(string path)
		{
			Path = path;
			Message = string.Format(CultureInfo.InvariantCulture, "publishArtifacts '{0}'", Path);
		}
	}
}