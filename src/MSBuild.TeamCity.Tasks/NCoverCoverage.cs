/*
 * Created by: egr
 * Created at: 26.04.2009
 * © 2007-2009 Alexander Egorov
 */

using System;
using System.Xml;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	internal struct NCoverAttribute
	{
		internal const string Coverage = "coverage";
		internal const string FunctionCoverage = "functionCoverage";
	}

	internal struct TeamCityStatisticKey
	{
		internal const string NCoverCoverage = "NCoverCoverage";
		internal const string NCoverFunctionCoverage = "NCoverCoverageF";
	}

	/// <summary>
	/// Represents NCoverExplorer statistic reporter into TeamCity
	/// </summary>
	/// <example>Reports statistic using NCoverExplorer summary report.
	/// <code><![CDATA[
	/// <NCoverCoverage NcoverReportPath="C:\SummaryReport.xml" />
	/// ]]></code>
	/// </example>
	public class NCoverCoverage : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to NCoverExplorer summmary report XML file
		/// </summary>
		[Required]
		public string NcoverReportPath { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			LogMessage("NCover xml report summary path \"" + NcoverReportPath + "\".");

			try
			{
				XmlReader reader = XmlReader.Create(NcoverReportPath);
				using ( reader )
				{
					reader.ReadToDescendant("project");

					WriteCoverageStatistic(reader, NCoverAttribute.Coverage, TeamCityStatisticKey.NCoverCoverage);
					WriteCoverageStatistic(reader, NCoverAttribute.FunctionCoverage, TeamCityStatisticKey.NCoverFunctionCoverage);
				}
			}
			catch ( Exception e )
			{
				Log.LogError(e.ToString());
			}
			return !Log.HasLoggedErrors;
		}

		private void LogMessage( string message )
		{
			try
			{
				Log.LogMessage(MessageImportance.High, message);
			}
			catch ( Exception e )
			{
				Console.WriteLine(e);
			}
		}

		private void WriteCoverageStatistic( XmlReader reader, string attribute, string property )
		{
			reader.MoveToAttribute(attribute);
			float coverage = reader.ReadContentAsFloat();
			BuildStatisticTeamCityMessage message = new BuildStatisticTeamCityMessage(property, coverage)
			                                        	{ IsAddTimeStamp = IsAddTimestamp, FlowId = FlowId };
			LogMessage(message.ToString());
		}
	}
}