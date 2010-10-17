/*
 * Created by: egr
 * Created at: 17.10.2010
 * © 2007-2010 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents compilationFinished TeamCity message
    ///</summary>
    public class CompilationFinishedMessage : TeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationFinishedMessage"/> class
        ///</summary>
        ///<param name="compiler">Compiler attribute value</param>
        public CompilationFinishedMessage(string compiler)
        {
            Attributes.Add("compiler", compiler);
        }


        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            get { return "compilationFinished"; }
        }
    }
}