/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents test start TC message
	///</summary>
	public class TestStartTeamCityMessage : NamedTeamCityMessage
	{
		///<summary>
		/// Creates new class instance using name specified
		///</summary>
		///<param name="name">Name attribute value</param>
		public TestStartTeamCityMessage( string name ) : base(name)
		{
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "testStarted"; }
		}
	}
}