/*
 * Created by: egr
 * Created at: 09.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Globalization;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents test finish TC message
	///</summary>
	public class TestFinishTeamCityMessage : NamedTeamCityMessage
	{
		///<summary>
		/// Creates new class instance using name and duration in seconds specified
		///</summary>
		///<param name="name">Name attribute value</param>
		///<param name="durationSeconds">Test duration in seconds</param>
		public TestFinishTeamCityMessage( string name, double durationSeconds ) : base(name)
		{
			double duration = durationSeconds * 1000;
			Attributes.Add(new MessageAttributeItem("duration", duration.ToString(CultureInfo.InvariantCulture)));
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "testFinished"; }
		}
	}
}