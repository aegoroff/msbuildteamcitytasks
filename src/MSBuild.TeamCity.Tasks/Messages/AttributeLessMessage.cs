/*
 * Created by: egr
 * Created at: 16.10.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents simple single TeamCity Message without attributes
    /// </summary>
    public class AttributeLessMessage : TeamCityMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AttributeLessMessage" /> class using message name specified
        /// </summary>
        /// <param name="message">TeamCity message name</param>
        public AttributeLessMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message { [DebuggerStepThrough] get; }
    }
}