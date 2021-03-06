/*
 * Created by: egr
 * Created at: 03.05.2009
 * � 2007-2015 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents simple single nameless attributed TeamCity Message
    /// </summary>
    public class SimpleTeamCityMessage : TeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SimpleTeamCityMessage" /> class using message name and text specified
        /// </summary>
        /// <param name="message">TeamCity message name</param>
        /// <param name="messageText">Build progress start message text</param>
        public SimpleTeamCityMessage(string message, string messageText)
        {
            this.Message = message;
            this.MessageText = messageText;
            this.Attributes.Add(new MessageAttributeItem(messageText));
        }

        /// <summary>
        ///     Gets message text
        /// </summary>
        public string MessageText { get; private set; }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message { [DebuggerStepThrough] get; }
    }
}