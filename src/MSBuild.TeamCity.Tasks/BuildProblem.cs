/*
 * Created by: egr
 * Created at: 22.06.2013
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     To fail a build directly from the build script, a build problem should be reported.
    /// </summary>
    /// <example>
    ///     Reports build problem using description specified
    ///     <code><![CDATA[
    /// <BuildProblem Description="Something nasty happend" />
    /// ]]></code>
    ///     Reports build problem full example (with all optional attributes)
    ///     <code><![CDATA[
    /// <BuildProblem 
    ///     Description="Something nasty happend" 
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Identity="tests"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildProblem : TeamCityTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BuildProblem" /> class
        /// </summary>
        public BuildProblem()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BuildProblem" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger">
        ///     <see cref="ILogger" /> implementation
        /// </param>
        public BuildProblem(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Gets or sets a human-readable plain text describing the build problem value
        /// </summary>
        /// <remarks>
        ///     Different problems should have different id, same problems - same id.
        ///     Shouldn't change throughout builds if the same problem occurs, e.g. the same compilation error.
        ///     Should be a valid Java id up to 60 characters.
        ///     If omitted, identity is calculated based on description text.
        /// </remarks>
        [Required]
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets a unique problem instance id.
        /// </summary>
        /// <remarks>
        ///     (optional) a unique problem instance id.
        ///     Different problems should have different id, same problems - same id.
        ///     Shouldn't change throughout builds if the same problem occurs, e.g. the same compilation error.
        ///     Should be a valid Java id up to 60 characters.
        ///     If omitted, identity is calculated based on description text.
        /// </remarks>
        public string Identity { get; set; }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new BuildProblemMessage(this.Description, this.Identity);
        }
    }
}