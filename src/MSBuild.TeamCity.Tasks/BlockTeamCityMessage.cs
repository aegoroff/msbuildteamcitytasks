/*
 * Created by: egr
 * Created at: 02.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represenst base class of Block* TeamCity messages.
	/// </summary>
	public abstract class BlockTeamCityMessage : TeamCityMessage
	{
		///<summary>
		/// Creates new message instance
		///</summary>
		///<param name="name">Block name</param>
		protected BlockTeamCityMessage( string name )
		{
			Name = name;
			Attributes.Add(new MessageAttribute("name", name));
		}

		///<summary>
		/// Gets block name
		///</summary>
		public string Name { get; private set; }
	}

	///<summary>
	/// Represents blockOpened TeamCity message
	///</summary>
	public class BlockOpenTeamCityMessage : BlockTeamCityMessage
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
	public class BlockCloseTeamCityMessage : BlockTeamCityMessage
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