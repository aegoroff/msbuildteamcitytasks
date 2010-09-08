/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Represents TeamCity task business logic class
	///</summary>
	public class TeamCityTaskImplementation
	{
		private const string TeamcityDiscoveryEnvVariable = "TEAMCITY_PROJECT_NAME";
		private readonly ILogger _logger;

		///<summary>
		/// Initializes a new instance of the <see cref="TeamCityTaskImplementation"/> class using 
		/// logger specified
		///</summary>
		///<param name="logger"><see cref="ILogger"/> implementation</param>
		public TeamCityTaskImplementation( ILogger logger )
		{
			_logger = logger;
		}

		/// <summary>
		/// Writes <see cref="TeamCityMessage"/> into MSBuild log using MessageImportance.High level
		/// </summary>
		/// <param name="message">Message to write</param>
		public void Write( TeamCityMessage message )
		{
			if ( !string.IsNullOrEmpty(Environment.GetEnvironmentVariable(TeamcityDiscoveryEnvVariable)) )
			{
				_logger.LogMessage(MessageImportance.High, message.ToString());
			}
		}

		/// <summary>
		/// Executes the task.
		/// </summary>
		/// <param name="result">A task execution result</param>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public bool Execute( ExecutionResult result )
		{
			return Execute(result, false, null);
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
		public bool Execute( ExecutionResult result, bool isAddTimestamp, string flowId )
		{
			if ( result.Messages != null )
			{
				foreach ( TeamCityMessage message in result.Messages )
				{
					message.FlowId = flowId;
					message.IsAddTimestamp = isAddTimestamp;
					Write(message);
				}
			}
			return result.Status;
		}
	}
}