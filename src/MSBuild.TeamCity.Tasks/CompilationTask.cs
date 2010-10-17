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
    /// Represents base class of all compilation tasks
    ///</summary>
    public abstract class CompilationTask : TeamCityTask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompilationTask"/> class
        /// </summary>
        protected CompilationTask()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationTask"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        protected CompilationTask( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets compiler name
        /// </summary>
        [Required]
        public string Compiler { get; set; }

        /// <summary>
        /// Gets the compliation message (start/finish)
        /// </summary>
        protected abstract string CompilationMessage { get; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new CompilationMessage(Compiler, CompilationMessage);
        }
    }

    ///<summary>
    /// Finishes reporting compilation messages using compiler specified. Available since TC 6.0
    ///</summary>
    /// <example>Finishes reporting compilation messages
    /// <code><![CDATA[
    /// ...
    /// <ReportMessage Text="compiler output" />
    /// <ReportMessage Text="compiler error" Status="ERROR"/>
    /// <CompilationFinished Compiler="javac"/>
    /// ]]></code>
    /// </example>
    public class CompilationFinished : CompilationTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationFinished"/> class
        ///</summary>
        public CompilationFinished()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="CompilationFinished"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public CompilationFinished( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets the compliation message (start/finish)
        /// </summary>
        protected override string CompilationMessage
        {
            get { return "compilationFinished"; }
        }
    }

    ///<summary>
    /// Starts reporting compilation messages using compiler specified. Available since TC 6.0
    ///</summary>
    /// <example>Starts reporting compilation messages
    /// <code><![CDATA[
    /// <CompilationStarted Compiler="javac"/>
    /// <ReportMessage Text="compiler output" />
    /// <ReportMessage Text="compiler error" Status="ERROR"/>
    /// ...
    /// ]]></code>
    /// </example>
    public class CompilationStarted : CompilationTask
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
        public CompilationStarted( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets the compliation message (start/finish)
        /// </summary>
        protected override string CompilationMessage
        {
            get { return "compilationStarted"; }
        }
    }
}