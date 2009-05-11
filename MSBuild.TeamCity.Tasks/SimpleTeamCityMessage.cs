/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents simple single nameless attributed TeamCity Message
	///</summary>
	public class SimpleTeamCityMessage : TeamCityMessage
	{
		private readonly string _message;
		
		///<summary>
		/// Creates new message instance using message name and text specified
		///</summary>
		///<param name="message">TeamCity message name</param>
		///<param name="messageText">Build progress start message text</param>
		public SimpleTeamCityMessage(string message, string messageText)
		{
			_message = message;
			MessageText = messageText;
			Attributes.Add(new MessageAttributeItem(messageText));
		}
		
		/// <summary>
		/// Message text
		/// </summary>
		public string MessageText { get; private set; }

		/// <summary>
		/// Gets message name
		/// </summary>
		protected override string Message
		{
			get { return _message; }
		}
	}
}