/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    ///<summary>
    /// Represents TeamCity task business logic class
    ///</summary>
    public class TeamCityTaskImplementation
    {
        private const string TeamcityDiscoveryEnvVariable = "TEAMCITY_PROJECT_NAME";
        private readonly ILogger logger;

        ///<summary>
        /// Initializes a new instance of the <see cref="TeamCityTaskImplementation"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public TeamCityTaskImplementation(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Writes <see cref="TeamCityMessage"/> into MSBuild log using MessageImportance.High level
        /// </summary>
        /// <param name="message">Message to write</param>
        public void Write(TeamCityMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable(TeamcityDiscoveryEnvVariable)))
            {
                this.logger.LogMessage(MessageImportance.High, message.ToString());
            }
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="result">A task execution result</param>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public bool Execute(ExecutionResult result)
        {
            return this.Execute(result, false, null);
        }

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="result">A task execution result</param>
        /// <param name="isAddTimestamp">a value indicating whether to add timestamt to the message</param>
        /// <param name="flowId">The flowId is a unique identifier of the messages flow in a build</param>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public bool Execute(ExecutionResult result, bool isAddTimestamp, string flowId)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }
            if (result.Messages == null)
            {
                return result.Status;
            }
            foreach (var message in result.Messages)
            {
                message.FlowId = flowId;
                message.IsAddTimestamp = isAddTimestamp;
                this.Write(message);
            }
            return result.Status;
        }
    }
}