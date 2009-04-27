/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
    /// Publish (upload) artifact(s) to JetBrains TeamCity server
    /// </summary>
    /// <example>Publish several artifacts.
    /// <code><![CDATA[
    /// <PublishArtifacts Artifacts="File1.zip;File2.zip" />
    /// ]]></code>
    /// </example>
	public class PublishArtifacts : Task
	{
		/// <summary>
        /// Gets or sets the artifacts to publish.
        /// </summary>
        /// <value>The artifacts.</value>
		[Required]
		public ITaskItem[] Artifacts { get; set; }

		/// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
		public override bool Execute()
		{
			foreach ( ITaskItem item in Artifacts )
			{
				PublishArtifactTeamCityMessage message = new PublishArtifactTeamCityMessage(item.ItemSpec);
				Log.LogMessage(MessageImportance.High, message.ToString());
			}
			return true;
		}
	}
}