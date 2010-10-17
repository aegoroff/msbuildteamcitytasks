/*
 * Created by: egr
 * Created at: 17.10.2010
 * © 2007-2010 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents compilationStarted TeamCity message
    ///</summary>
    public class CompilationStartedMessage : TeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationStartedMessage"/> class
        ///</summary>
        ///<param name="compiler">Compiler attribute value</param>
        public CompilationStartedMessage(string compiler)
        {
            Attributes.Add("compiler", compiler);
        }

        
        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return "compilationStarted"; }
        }
    }
}