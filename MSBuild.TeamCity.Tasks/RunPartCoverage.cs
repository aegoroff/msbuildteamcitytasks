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
	/// Runs code coverage using PartCover tool (http://partcover.blogspot.com/) and exports results into TC.
	/// </summary>
	public class RunPartCoverage : TeamCityTask
	{
		/// <summary>
		/// Gets or sets full path to PartCover installation folder
		/// </summary>
		[Required]
		public string ToolPath { get; set; }

		/// <summary>
		/// Gets or sets full path to path to executable file to count coverage
		/// </summary>
		[Required]
		public string TargetPath { get; set; }

		/// <summary>
		/// Gets or sets arguments for target process
		/// </summary>
		[Required]
		public string TargetArguments { get; set; }

		/// <summary>
		/// Gets or sets xslt transformation rules one per line (use ; as separator) in the following format: file.xslt=>generatedFileName.html
		/// </summary>
		public ITaskItem[] ReportXslts { get; set; }

		/// <summary>
		/// Gets or sets items to include to coverate (use ; as separator in case of many)
		/// </summary>
		public ITaskItem[] Includes { get; set; }

		/// <summary>
		/// Gets or sets items to exclude from coverate (use ; as separator in case of many)
		/// </summary>
		public ITaskItem[] Excludes { get; set; }

		/// <summary>
		/// Reads TeamCity messages
		/// </summary>
		/// <returns>TeamCity messages list</returns>
		protected override IEnumerable<TeamCityMessage> ReadMessages()
		{
			if ( ReportXslts != null )
			{
				SequenceBuilder<string> builder = new SequenceBuilder<string>(EnumerateReports(), "\n");
				yield return new DotNetCoverMessage(DotNetCoverMessage.PartcoverReportXsltsKey, builder.ToString());
			}
			yield return new ImportDataTeamCityMessage(ImportType.DotNetCoverage,
			                                           string.Empty, // TODO: Pass real path
			                                           DotNetCoverageTool.PartCover);
		}

		private IEnumerable<string> EnumerateReports()
		{
			foreach ( ITaskItem report in ReportXslts )
			{
				yield return report.ItemSpec;
			}
		}
	}
}