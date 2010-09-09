/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Publish (upload) artifact(s) to JetBrains TeamCity server
	/// </summary>
	/// <example>Publish several artifacts.
	/// <code><![CDATA[
	/// <PublishArtifacts
	///		Artifacts="File1.zip;File2.zip"
	/// />
	/// ]]></code>
	/// Publish several artifacts full example (with all optional attributes)
	/// <code><![CDATA[
	/// <PublishArtifacts
	///		IsAddTimestamp="true"
	///		FlowId="1"
	///		Artifacts="File1.zip;File2.zip"
	/// />
	/// ]]></code>
	/// </example>
	public class PublishArtifacts : TeamCityTask
	{
		///<summary>
		/// Initializes a new instance of the <see cref="PublishArtifacts"/> class
		///</summary>
		public PublishArtifacts()
		{
		}

		///<summary>
		/// Initializes a new instance of the <see cref="PublishArtifacts"/> class using 
		/// logger specified
		///</summary>
		///<param name="logger"><see cref="ILogger"/> implementation</param>
		public PublishArtifacts( ILogger logger )
			: base(logger)
		{
		}

		/// <summary>
		/// Gets or sets the artifacts to publish.
		/// </summary>
		/// <value>The artifacts.</value>
		[Required]
		public ITaskItem[] Artifacts { get; set; }

		/// <summary>
		/// Reads TeamCity messages
		/// </summary>
		/// <returns>TeamCity messages list</returns>
		protected override IEnumerable<TeamCityMessage> ReadMessages()
		{
			foreach ( ITaskItem item in Artifacts )
			{
				yield return new SimpleTeamCityMessage("publishArtifacts", item.ItemSpec);
			}
		}
	}
}