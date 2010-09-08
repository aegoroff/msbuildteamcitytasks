/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represent abstract TeamCity task. Cannot be used directly (because it's abstract) in MSBuild script
	/// </summary>
	public abstract class TeamCityTask : Task
	{
		private readonly ILogger _logger;
		private readonly TeamCityTaskImplementation _implementation;

		/// <summary>
		/// Initializes a new instance of the <see cref="TeamCityTask"/> class
		/// </summary>
		protected TeamCityTask()
		{
			_logger = new Logger(Log);
			_implementation = new TeamCityTaskImplementation(_logger);
		}

		/// <summary>
		/// Gets or sets a value indicating whether to add message's timestamp attribute. False by default
		/// </summary>
		public bool IsAddTimestamp { get; set; }

		/// <summary>
		/// Gets or sets message flowId. The flowId is a unique identifier of the messages flow in a build. 
		/// Flow tracking is necessary for example to distinguish separate processes running in parallel. 
		/// The identifier is a string that should be unique in the scope of individual build.
		/// </summary>
		public string FlowId { get; set; }

		/// <summary>
		/// Gets logging object
		/// </summary>
		protected ILogger Logger
		{
			get { return _logger; }
		}

		/// <summary>
		/// Gets task execution result
		/// </summary>
		protected virtual ExecutionResult ExecutionResult
		{
			get
			{
				ExecutionResult result = new ExecutionResult
				{
					Status = true,
					Messages = new List<TeamCityMessage>()
				};
				foreach ( TeamCityMessage message in ReadMessages() )
				{
					result.Messages.Add(message);
				}
				return result;
			}
		}

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			try
			{
				return _implementation.Execute(ExecutionResult, IsAddTimestamp, FlowId);
			}
			catch ( Exception e )
			{
				Console.WriteLine(e);
			}
			return false;
		}

		/// <summary>
		/// Reads TeamCity messages
		/// </summary>
		/// <returns>TeamCity messages list</returns>
		protected virtual IEnumerable<TeamCityMessage> ReadMessages()
		{
			return new List<TeamCityMessage>();
		}
	}
}