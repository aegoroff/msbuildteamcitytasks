/*
 * Created by: egr
 * Created at: 22.06.2013
 * © 2007-2013 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents buildProblem TC message
    /// </summary>
    public class BuildProblemMessage : TeamCityMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProblemMessage"/> class
        /// </summary>
        /// <param name="description">(mandatory) a human-readable plain text describing the build problem. 
        /// By default description appears in the build status text and in the list of build's problems. 
        /// Limited to 4000 symbols, will be truncated if exceeds.</param>
        /// <param name="identity">(optional) a unique problem instance id. 
        /// Different problems should have different id, same problems - same id. 
        /// Shouldn't change throughout builds if the same problem occurs, e.g. the same compilation error. 
        /// Should be a valid Java id up to 60 characters. 
        /// If omitted, identity is calculated based on description text.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        public BuildProblemMessage(string description, string identity = null)
        {
            Attributes.Add("description", description);
            if (!string.IsNullOrWhiteSpace(identity))
            {
                Attributes.Add("identity", identity);
            }
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough]
            get { return "buildProblem"; }
        }
    }
}