/*
 * Created by: egr
 * Created at: 07.09.2010
 * © 2007-2010 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Manually configures .NET coverage processing using NCover3 tool.
	/// See http://confluence.jetbrains.net/display/TCD5/Manually+Configuring+Reporting+Coverage
	///</summary>
	/// <example>Configures .NET coverage processing using NCover3 tool
	/// <code><![CDATA[
	/// <NCover3Report
	///		ToolPath="C:\Program Files\NCover3"
	///		XmlReportPath="D:\project\ncover3.xml"
	/// />
	/// ]]></code>
	/// Configures .NET coverage processing using NCover3 tool and specifying arguments for NCover report generator
	/// <code><![CDATA[
	/// <NCover3Report
	///		ToolPath="C:\Program Files\NCover3"
	///		XmlReportPath="D:\project\ncover3.xml"
	///		Arguments="FullCoverageReport:Html:{teamcity.report.path}"
	/// />
	/// ]]></code>
	/// </example>	
	public class NCover3Report : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to NCover 3 installation folder
		/// </summary>
		[Required]
		public string ToolPath { get; set; }
		
		/// <summary>
		/// Gets or sets full path to xml report file that was created by NCover3
		/// </summary>
		[Required]
		public string XmlReportPath { get; set; }

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
			Write(new ImportDataTeamCityMessage(ImportType.DotNetCoverage, XmlReportPath, DotNetCoverageTool.Ncover3));
			return true;
		}
	}
}