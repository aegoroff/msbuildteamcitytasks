/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Represent base class of all BuildProgress* tasks. Cannot be used directly in MSBuild script (because it's abstract).
    ///</summary>
    public abstract class BuildProgressTask : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressTask"/> class
        ///</summary>
        protected BuildProgressTask()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressTask"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        protected BuildProgressTask( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets progress message text
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected abstract string MessageName { get; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new SimpleTeamCityMessage(MessageName, Message);
        }
    }
}