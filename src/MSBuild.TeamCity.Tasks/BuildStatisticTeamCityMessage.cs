/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	public class BuildStatisticTeamCityMessage : TeamCityMessage
	{
		public BuildStatisticTeamCityMessage(string key, float value)
		{
			Key = key;
			Value = value;
			Message = string.Format(CultureInfo.InvariantCulture, "buildStatisticValue key='{0}' value='{1:F}'", Key, Value);
		}
		
		public string Key { get; private set; }
		public float Value { get; private set; }
	}
}