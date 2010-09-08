/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
 */

using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	///<summary>
	/// Manually configures .NET coverage processing using NCover 1.x tool.
	/// See http://confluence.jetbrains.net/display/TCD5/Manually+Configuring+Reporting+Coverage
	///</summary>
	/// <example>Configures .NET coverage processing using NCover 1.x tool
	/// <code><![CDATA[
	/// <NCoverReportTask
	///		NCoverExplorerPath="C:\Program Files\NCoverExplorer"
	///		XmlReportPath="D:\project\ncover.xml"
	/// />
	/// ]]></code>
	/// Configures .NET coverage processing using 1.x tool and specifying arguments for NCover report generator
	/// <code><![CDATA[
	/// <NCoverReportTask
	///		NCoverExplorerPath="C:\Program Files\NCoverExplorer"
	///		XmlReportPath="D:\project\ncover.xml"
	///		Arguments="arguments"
	/// />
	/// ]]></code>
	/// </example>	
	public class NCoverReportTask : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to NCover Explorer toot installation folder
		/// </summary>
		[Required]
		public string NCoverExplorerPath { get; set; }

		/// <summary>
		/// Gets or sets full path to xml report file that was created by NCover 1.x
		/// </summary>
		[Required]
		public string XmlReportPath { get; set; }

		///<summary>
		/// Gets or sets additional arguments for NCover 1.x
		///</summary>
		public string Arguments { get; set; }

		/// <summary>
		/// Gets or sets value for /report: argument.
		/// </summary>
		public string ReportType { get; set; }

		/// <summary>
		/// Gets or sets value for /sort: argument
		/// </summary>
		public string ReportOrder { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			Write(new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerToolKey, NCoverExplorerPath));
			if ( !string.IsNullOrEmpty(Arguments) )
			{
				Write(new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerToolArgsKey, Arguments));
			}
			if ( !string.IsNullOrEmpty(ReportType) )
			{
				Write(new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerReportTypeKey, ReportType));
			}
			if ( !string.IsNullOrEmpty(ReportOrder) )
			{
				Write(new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerReportOrderKey, ReportOrder));
			}
			Write(new ImportDataTeamCityMessage(ImportType.DotNetCoverage, XmlReportPath, DotNetCoverageTool.Ncover));
			return true;
		}
	}
}