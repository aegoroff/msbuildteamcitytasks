/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	public class TeamCityMessage
	{
		protected string Message { get; set; }

		public TeamCityMessage()
		{
		}
		
		public TeamCityMessage(string message)
		{
			Message = message;
		}

		public override string ToString()
		{
			return string.Format("##teamcity[{0}]", Message);
		}
	}
}