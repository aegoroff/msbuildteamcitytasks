/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks
{
	public class BuildNumber : Task
	{
		[Required]
		public string Number { get; set; }

		public override bool Execute()
		{
			BuildNumberTeamCityMessage message = new BuildNumberTeamCityMessage(Number);
			Log.LogMessage(MessageImportance.High, message.ToString());
			return true;
		}
	}
}