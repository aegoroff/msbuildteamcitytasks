/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
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
		/// Writes <see cref="TeamCityMessage"/> into MSBuild log using MessageImportance.High level
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void Write( TeamCityMessage message )
		{
			message.IsAddTimestamp = IsAddTimestamp;
			message.FlowId = FlowId;
			_implementation.Write(message);
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
				return _implementation.Execute(ExecutionResult);
			}
			catch ( Exception e )
			{
				Console.WriteLine(e);
			}
			return false;
		}

		/// <summary>
		/// Gets task execution result
		/// </summary>
		protected virtual ExecutionResult ExecutionResult
		{
			get { return new ExecutionResult { Status = true }; }
		}
	}
}