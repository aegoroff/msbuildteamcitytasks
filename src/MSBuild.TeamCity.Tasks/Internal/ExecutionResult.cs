/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Represents a task execution result
    /// </summary>
    public class ExecutionResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExecutionResult" /> class using
        ///     status specified
        /// </summary>
        /// <param name="status">
        ///     a value indicating whether the import was successful.
        ///     True if the operation was successful and there are no failing tests
        ///     otherwise false
        /// </param>
        public ExecutionResult(bool status)
        {
            this.Status = status;
        }

        /// <summary>
        ///     Gets or sets <see cref="TeamCityMessage" /> to output
        /// </summary>
        public IList<TeamCityMessage> Messages { [DebuggerStepThrough] get; } = new List<TeamCityMessage>();

        /// <summary>
        ///     Gets or sets a value indicating whether the import was successful.
        ///     True if the operation was successful and there are no failing tests
        ///     otherwise false
        /// </summary>
        public bool Status { get; set; }
    }
}