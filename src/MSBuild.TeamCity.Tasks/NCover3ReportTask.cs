/*
 * Created by: egr
 * Created at: 07.09.2010
 * © 2007-2010 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Manually configures .NET coverage processing
	///</summary>
	/// <example>Configures .NET coverage processing using NCover3 tool using full path to NCover3 installation folder
	/// <code><![CDATA[
	/// <NCover3ReportTask
	///		ToolPath="C:\Program Files\NCover3"
	/// />
	/// ]]></code>
	/// Configures .NET coverage processing using NCover3 tool using full path to NCover3 installation folder and arguments for NCover report generator
	/// <code><![CDATA[
	/// <NCover3ReportTask
	///		ToolPath="C:\Program Files\NCover3"
	///		Arguments="FullCoverageReport:Html:{teamcity.report.path}"
	/// />
	/// ]]></code>
	/// </example>	
	public class NCover3ReportTask : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to full path to NCover 3 installation folder
		/// </summary>
		[Required]
		public string ToolPath { get; set; }

		///<summary>
		/// Gets or sets arguments for NCover report generator. //or FullCoverageReport:Html:{teamcity.report.path}
		///</summary>
		public string Arguments { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new DotNetCoverMessage(DotNetCoverMessage.NCover3HomeKey, ToolPath));
			if ( !string.IsNullOrEmpty(Arguments) )
			{
				Write(new DotNetCoverMessage(DotNetCoverMessage.NCover3ReporterArgsKey, Arguments));
			}
			return true;
		}
	}
}