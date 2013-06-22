/*
 * Created by: egr
 * Created at: 17.10.2010
 * © 2007-2012 Alexander Egorov
 */

using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents compilationStarted TeamCity message
    ///</summary>
    public class CompilationMessage : TeamCityMessage
    {
        private readonly string message;

        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationMessage"/> class
        ///</summary>
        ///<param name="compiler">Compiler attribute value</param>
        ///<param name="message">Compilation start/finish message</param>
        public CompilationMessage(string compiler, string message)
        {
            this.message = message;
            Attributes.Add("compiler", compiler);
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough]
            get { return message; }
        }
    }
}