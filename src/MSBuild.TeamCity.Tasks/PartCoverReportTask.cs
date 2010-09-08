/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Manually configures .NET coverage processing using PartCover tool.
	/// See http://confluence.jetbrains.net/display/TCD5/Manually+Configuring+Reporting+Coverage
	/// </summary>
	/// <example>Configures .NET coverage processing using PartCover tool
	/// <code><![CDATA[
	/// <PartCoverReportTask
	///		XmlReportPath="D:\project\partcover.xml"
	/// />
	/// ]]></code>
	/// Configures .NET coverage processing using PartCover tool and xslt transformation rules
	/// <code><![CDATA[
	/// <PartCoverReportTask
	///		ReportXslts="file.xslt=>generatedFileName.html;file1.xslt=>generatedFileName1.html"
	///		XmlReportPath="D:\project\partcover.xml"
	/// />
	/// ]]></code>
	/// </example>	
	public class PartCoverReportTask : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to xml report file that was created by PartCover
		/// </summary>
		[Required]
		public string XmlReportPath { get; set; }

		/// <summary>
		/// Gets or sets xslt transformation rules one per line (use ; as separator) in the following format: file.xslt=>generatedFileName.html
		/// </summary>
		public ITaskItem[] ReportXslts { get; set; }

		/// <summary>
		/// When overridden in a derived class, executes the task.
		/// </summary>
		/// <returns>
		/// true if the task successfully executed; otherwise, false.
		/// </returns>
		public override bool Execute()
		{
			if ( ReportXslts != null )
			{
				SequenceBuilder<string> builder = new SequenceBuilder<string>(EnumerateReports(), "\n");
				Write(new DotNetCoverMessage(DotNetCoverMessage.PartcoverReportXsltsKey, builder.ToString()));
			}
			Write(new ImportDataTeamCityMessage(ImportType.DotNetCoverage, XmlReportPath, DotNetCoverageTool.PartCover));
			return true;
		}

		private IEnumerable<string> EnumerateReports()
		{
			foreach (ITaskItem report in ReportXslts)
			{
				yield return report.ItemSpec;
			}
		}
	}
}