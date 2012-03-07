/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2012 Alexander Egorov
 */

using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Just wrapper over <see cref="TaskLoggingHelper"/> class to simplify testing
    ///</summary>
    internal sealed class Logger : ILogger
    {
        private readonly TaskLoggingHelper loggingHelper;

        ///<summary>
        /// Initializes a new instance of the <see cref="Logger"/> class using 
        /// <see cref="TaskLoggingHelper"/> instance.
        ///</summary>
        ///<param name="loggingHelper"><see cref="TaskLoggingHelper"/> to wrap up</param>
        public Logger(TaskLoggingHelper loggingHelper)
        {
            this.loggingHelper = loggingHelper;
        }

        /// <summary>
        /// Gets a value indicating whether the task has logged any errors through this logging helper object.
        /// </summary>
        public bool HasLoggedErrors
        {
            get { return loggingHelper.HasLoggedErrors; }
        }

        /// <summary>
        /// Logs an error using the message, and optionally the stack trace, from the given exception.
        /// </summary>
        /// <param name="exception">The exception to log</param>
        /// <param name="showStackTrace">true to include the stack trace in the log; otherwise, false.</param>
        public void LogErrorFromException(Exception exception, bool showStackTrace)
        {
            loggingHelper.LogErrorFromException(exception, showStackTrace);
        }

        /// <summary>
        /// Logs a message with the specified string.
        /// </summary>
        /// <param name="importance">One of the enumeration values that specifies the importance of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The arguments for formatting the message.</param>
        public void LogMessage(MessageImportance importance, string message, params object[] messageArgs)
        {
            loggingHelper.LogMessage(importance, message, messageArgs);
        }
    }
}