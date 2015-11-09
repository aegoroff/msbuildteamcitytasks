/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Build.Utilities;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;
using Logger = MSBuild.TeamCity.Tasks.Internal.Logger;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Represent abstract TeamCity task. Cannot be used directly (because it's abstract) in MSBuild script
    /// </summary>
    public abstract class TeamCityTask : Task
    {
        private TeamCityTaskImplementation implementation;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeamCityTask" /> class
        /// </summary>
        protected TeamCityTask()
        {
            this.Initialize(new Logger(this.Log));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TeamCityTask" /> class using logger implementation specified
        /// </summary>
        /// <param name="logger">
        ///     Logger implementation
        /// </param>
        protected TeamCityTask(ILogger logger)
        {
            this.Initialize(logger);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether to add message's timestamp attribute. False by default
        /// </summary>
        public bool IsAddTimestamp { get; set; }

        /// <summary>
        ///     Gets or sets message flowId. The flowId is a unique identifier of the messages flow in a build.
        ///     Flow tracking is necessary for example to distinguish separate processes running in parallel.
        ///     The identifier is a string that should be unique in the scope of individual build.
        /// </summary>
        public string FlowId { get; set; }

        /// <summary>
        ///     Gets logging object
        /// </summary>
        protected ILogger Logger { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the operation status success or failure
        /// </summary>
        protected virtual bool ExecutionStatus => true;

        /// <summary>
        ///     When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        ///     true if the task successfully executed; otherwise, false.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override bool Execute()
        {
            try
            {
                var messages = this.ReadMessages();
                var result = new ExecutionResult(this.ExecutionStatus);
                result.Messages.AddRange(messages);
                return this.implementation.Execute(result, this.IsAddTimestamp, this.FlowId);
            }
            catch (Exception e)
            {
                this.Logger.LogErrorFromException(e, true);
            }
            return false;
        }

        /// <summary>
        ///     Reads TeamCity messages. Empty by default (if not overriden)
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected abstract IEnumerable<TeamCityMessage> ReadMessages();

        private void Initialize(ILogger logger)
        {
            this.Logger = logger;
            this.implementation = new TeamCityTaskImplementation(logger);
        }
    }
}