/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Runs code coverage using PartCover tool (http://partcover.blogspot.com/) and exports results into TC.
	/// </summary>
	/// <example>Working example of code coverage using PartCover and imports test results into TC
	/// <code><![CDATA[
	/// <RunPartCoverage
	///		ToolPath='C:\Program Files\Gubka Bob\PartCover .NET 2.3'
	///		TargetPath='C:\Program Files\NUnit 2.5.2\bin\net-2.0\nunit-console.exe'
	///		XmlReportPath='$(MSBuildProjectDirectory)\partcover.xml'
	///		TargetArguments="$(MSBuildProjectDirectory)\Tests\bin\$(Configuration)\Tests.dll"
	///		Includes='[MSBuild.TeamCity.Tasks]*'
	///		ReportXslts='C:\Program Files\Gubka Bob\PartCover .NET 2.3\xslt\Report By Class.xslt=>CoverageSummary.html'
	///	/>
	/// ]]></code>
	/// </example>
	public class RunPartCoverage : PartCoverReport
	{
		private const string PartCoverExe = "PartCover.exe";

		///<summary>
		/// Initializes a new instance of the <see cref="RunPartCoverage"/> class
		///</summary>
		public RunPartCoverage()
		{
		}

		///<summary>
		/// Initializes a new instance of the <see cref="RunPartCoverage"/> class using 
		/// logger specified
		///</summary>
		///<param name="logger"><see cref="ILogger"/> implementation</param>
		public RunPartCoverage( ILogger logger )
			: base(logger)
		{
		}

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
		/// Gets or sets path to working directory to target process
		/// </summary>
		public string TargetWorkDir { get; set; }

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
			PartCoverCommandLine commandLine = new PartCoverCommandLine
			                                   	{
			                                   		Target = TargetPath,
			                                   		TargetWorkDir = TargetWorkDir,
			                                   		TargetArguments = TargetArguments,
													Output = XmlReportPath,
			                                   	};
			if ( Includes != null )
			{
				( (List<string>) commandLine.Includes ).AddRange(Enumerate(Includes));
			}
			if ( Excludes != null )
			{
				( (List<string>) commandLine.Excludes ).AddRange(Enumerate(Excludes));
			}

			string partCoverExePath = Path.Combine(ToolPath, PartCoverExe);
			ProcessRunner runner = new ProcessRunner(partCoverExePath);
			runner.Run(commandLine.ToString());

			return base.ReadMessages();
		}
	}
}