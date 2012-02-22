/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2012 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Represents MSbuild task logging interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets a value indicating whether the task has logged any errors through this logging helper object.
        /// </summary>
        bool HasLoggedErrors { get; }

        /// <summary>
        /// Logs an error with the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">Optional arguments for formatting the message string.</param>
        void LogError(string message, params object[] messageArgs);

        /// <summary>
        /// Logs a message with the specified string.
        /// </summary>
        /// <param name="importance">One of the enumeration values that specifies the importance of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="messageArgs">The arguments for formatting the message.</param>
        void LogMessage(MessageImportance importance, string message, params object[] messageArgs);
    }
}