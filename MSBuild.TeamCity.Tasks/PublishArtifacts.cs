/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks
{
	public class PublishArtifacts : Task
	{
		[Required]
		public string[] Artifacts { get; set; }

		public override bool Execute()
		{
			foreach ( string artifact in Artifacts )
			{
				PublishArtifactTeamCityMessage message = new PublishArtifactTeamCityMessage(artifact);
				Log.LogMessage(MessageImportance.High, message.ToString());
			}
			return true;
		}
	}
}