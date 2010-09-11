/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
	/// <summary>
	/// Represenst base class of all TeamCity messages that have name attribute.
	/// </summary>
	public abstract class NamedTeamCityMessage : TeamCityMessage
	{
		///<summary>
		/// Initializes a new instance of the <see cref="NamedTeamCityMessage"/> class
		///</summary>
		///<param name="name">Name attribute value</param>
		protected NamedTeamCityMessage( string name )
		{
			Attributes.Add(new MessageAttributeItem("name", name));
		}
	}
}