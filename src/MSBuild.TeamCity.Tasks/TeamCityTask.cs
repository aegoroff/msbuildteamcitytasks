/*
 * Created by: egr
 * Created at: 01.05.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represent abstract TeamCity task. Cannot be used directly (because it's abstract) in MSBuild script
	/// </summary>
	public abstract class TeamCityTask : Task
	{
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
		/// Writes <see cref="TeamCityMessage"/> into MSBuild log using MessageImportance.High level
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void Write( TeamCityMessage message )
		{
			message.IsAddTimestamp = IsAddTimestamp;
			message.FlowId = FlowId;
			LogMessage(message.ToString());
		}

		/// <summary>
		/// Writes message into MSBuild log using MessageImportance.High level
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void LogMessage( string message )
		{
			try
			{
				if ( !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_PROJECT_NAME")) )
				{
					Log.LogMessage(MessageImportance.High, message);
				}
			}
			catch ( Exception e )
			{
				Console.WriteLine(e);
			}
		}
	}
}