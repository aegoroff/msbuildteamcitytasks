/*
 * Created by: egr
 * Created at: 16.10.2010
 * © 2007-2012 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents simple single TeamCity Message without attributes
    ///</summary>
    public class AttributeLessMessage : TeamCityMessage
    {
        private readonly string message;

        ///<summary>
        /// Initializes a new instance of the <see cref="AttributeLessMessage"/> class using message name specified
        ///</summary>
        ///<param name="message">TeamCity message name</param>
        public AttributeLessMessage(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return message; }
        }
    }
}