/*
 * Created by: egr
 * Created at: 02.05.2009
 * � 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents blockOpened TeamCity message
	///</summary>
	public class BlockOpenTeamCityMessage : NamedTeamCityMessage
	{
		///<summary>
		/// Creates new message instance
		///</summary>
		///<param name="name">Block name</param>
		public BlockOpenTeamCityMessage( string name ) : base(name)
		{
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "blockOpened"; }
		}
	}

	///<summary>
	/// Represents blockOpened TeamCity message
	///</summary>
	public class BlockCloseTeamCityMessage : NamedTeamCityMessage
	{
		///<summary>
		/// Creates new message instance
		///</summary>
		///<param name="name">Block name</param>
		public BlockCloseTeamCityMessage( string name )
			: base(name)
		{
		}

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return "blockClosed"; }
		}
	}
}