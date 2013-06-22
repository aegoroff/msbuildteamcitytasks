/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using System.Diagnostics;
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
        /// Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BuildProgressTask"/> class
        ///</summary>
        protected BuildProgressTask()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BuildProgressTask"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        protected BuildProgressTask(ILogger logger)
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

    /// <summary>
    /// Writes progress start message into TeamCity log
    /// </summary>
    /// <example>Writes progress start message into TeamCity log
    /// <code><![CDATA[
    /// <BuildProgressStart Message="Message text" />
    /// ]]></code>
    /// Writes progress start message into TeamCity log full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BuildProgressStart
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Message="Message text"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildProgressStart : BuildProgressTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BuildProgressStart"/> class
        ///</summary>
        public BuildProgressStart()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BuildProgressStart"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BuildProgressStart(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string MessageName
        {
            [DebuggerStepThrough]
            get { return "progressStart"; }
        }
    }

    /// <summary>
    /// Writes progress finish message into TeamCity log
    /// </summary>
    /// <example>Writes progress finish message into TeamCity log
    /// <code><![CDATA[
    /// <BuildProgressFinish Message="Message text" />
    /// ]]></code>
    /// Writes progress finish message into TeamCity log full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BuildProgressFinish
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Message="Message text"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildProgressFinish : BuildProgressTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressFinish"/> class
        ///</summary>
        public BuildProgressFinish()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="BuildProgressFinish"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BuildProgressFinish(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string MessageName
        {
            [DebuggerStepThrough]
            get { return "progressFinish"; }
        }
    }

    /// <summary>
    /// Writes progress message into TeamCity log
    /// </summary>
    /// <example>Writes progress message into TeamCity log
    /// <code><![CDATA[
    /// <BuildProgressMessage Message="Message text" />
    /// ]]></code>
    /// Writes progress message into TeamCity log full example (with all optional attributes)
    /// <code><![CDATA[
    /// <BuildProgressMessage
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Message="Message text"
    /// />
    /// ]]></code>
    /// </example>
    public class BuildProgressMessage : BuildProgressTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BuildProgressMessage"/> class
        ///</summary>
        public BuildProgressMessage()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="MSBuild.TeamCity.Tasks.BuildProgressMessage"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public BuildProgressMessage(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string MessageName
        {
            [DebuggerStepThrough]
            get { return "progressMessage"; }
        }
    }
}