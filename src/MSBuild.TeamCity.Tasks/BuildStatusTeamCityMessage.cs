/*
 * Created by: egr
 * Created at: 03.05.2009
 * � 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents TeamCity build status setting message.
	/// </summary>
	public class BuildStatusTeamCityMessage : TeamCityMessage
	{
		///<summary>
		/// Creates new message instance using status and text specified.
		///</summary>
		///<param name="status">The status attribute may take following values: FAILURE, SUCCESS.</param>
		///<param name="text">Some useful status text</param>
		public BuildStatusTeamCityMessage( string status, string text )
		{
			Attributes.Add(new MessageAttribute("status", status));
			Attributes.Add(new MessageAttribute("text", text));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "buildStatus"; }
		}
	}
}