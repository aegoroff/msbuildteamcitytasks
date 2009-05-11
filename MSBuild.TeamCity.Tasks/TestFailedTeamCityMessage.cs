/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents test failure TC message
	///</summary>
	public class TestFailedTeamCityMessage : NamedTeamCityMessage
	{
		///<summary>
		/// Creates new class instance
		///</summary>
		///<param name="name">Test name</param>
		///<param name="message">Failure message</param>
		///<param name="details">Failure details like stack trace</param>
		public TestFailedTeamCityMessage( string name, string message, string details ) : base(name)
		{
			Attributes.Add(new MessageAttributeItem("message", message));
			Attributes.Add(new MessageAttributeItem("details", details));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "testFailed"; }
		}
	}
}