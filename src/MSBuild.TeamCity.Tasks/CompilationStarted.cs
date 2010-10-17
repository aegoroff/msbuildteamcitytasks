/*
 * Created by: egr
 * Created at: 17.10.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Starts reporting compilation messages using compiler specified
    ///</summary>
    public class CompilationStarted : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationStarted"/> class
        ///</summary>
        public CompilationStarted()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationStarted"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public CompilationStarted(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets compiler name
        /// </summary>
        [Required]
        public string Compiler { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new CompilationMessage(Compiler, "compilationStarted");
        }
    }
}