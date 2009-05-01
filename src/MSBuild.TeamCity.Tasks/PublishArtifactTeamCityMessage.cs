/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents TC message that publishes artifacts
	/// </summary>
	public class PublishArtifactTeamCityMessage : TeamCityMessage
	{
		/// <summary>
		/// Gets artifact path to publish
		/// </summary>
		public string Path { get; private set; }

		/// <summary>
		/// Creates new message instance using artifact path specified.
		/// </summary>
		/// <param name="path">Artifact path to publish</param>
		public PublishArtifactTeamCityMessage(string path)
		{
			Path = path;
			Attributes.Add(new MessageAttribute(path));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "publishArtifacts"; }
		}
	}
}