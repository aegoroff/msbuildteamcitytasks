/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2011 Alexander Egorov
 */

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Just wrapper over <see cref="TaskLoggingHelper"/> class to simplify testing
    ///</summary>
    internal sealed class Logger : ILogger
    {
        private readonly TaskLoggingHelper _loggingHelper;

        ///<summary>
        /// Initializes a new instance of the <see cref="Logger"/> class using 
        /// <see cref="TaskLoggingHelper"/> instance.
        ///</summary>
        ///<param name="loggingHelper"><see cref="TaskLoggingHelper"/> to wrap up</param>
        public Logger( TaskLoggingHelper loggingHelper )
        {
            _loggingHelper = loggingHelper;
        }

        /// <summary>
        /// Gets a value indicating whether the task has logged any errors through this logging helper object.
        /// </summary>
        public bool HasLoggedErrors
        {
            get { return _loggingHelper.HasLoggedErrors; }
        }

        /// <summary>
        /// Logs an error with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">Optional arguments for formatting the message string.</param>
        public void LogError( string message, params object[] messageArgs )
        {
            _loggingHelper.LogError(message, messageArgs);
        }

        /// <summary>
        /// Logs a message with the specified string.
        /// </summary>
        /// <param name="importance">One of the enumeration values that specifies the importance of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The arguments for formatting the message.</param>
        public void LogMessage( MessageImportance importance, string message, params object[] messageArgs )
        {
            _loggingHelper.LogMessage(importance, message, messageArgs);
        }
    }
}