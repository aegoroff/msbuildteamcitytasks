/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2011 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Manually configures .NET coverage processing using NCover 1.x tool.
    /// See http://confluence.jetbrains.net/display/TCD5/Manually+Configuring+Reporting+Coverage
    ///</summary>
    /// <example>Configures .NET coverage processing using NCover 1.x tool
    /// <code><![CDATA[
    /// <NCoverReport
    ///     NCoverExplorerPath="C:\Program Files\NCoverExplorer"
    ///     XmlReportPath="D:\project\ncover.xml"
    /// />
    /// ]]></code>
    /// Configures .NET coverage processing using NCover 1.x tool full example
    /// <code><![CDATA[
    /// <NCoverReport
    ///     NCoverExplorerPath="C:\Program Files\NCoverExplorer"
    ///     XmlReportPath="D:\project\ncover.xml"
    ///     Arguments="arguments"
    ///     ReportType="ModuleClassFunctionSummary"
    ///     ReportOrder="0"
    /// />
    /// ]]></code>
    /// </example>
    public class NCoverReport : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="NCoverReport"/> class
        ///</summary>
        public NCoverReport()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="NCoverReport"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public NCoverReport( ILogger logger )
            : base(logger)
        {
        }

        /// <summary>
        /// Gets or sets full path to NCover Explorer tool installation folder
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
        /// <remarks>
        /// can accept following values: None, ModuleSummary, ModuleNamespaceSummary, 
        /// ModuleClassSummary, ModuleClassFunctionSummary, 
        /// ModuleClassFunctionSummary
        /// </remarks>
        public string ReportType { get; set; }

        /// <summary>
        /// Gets or sets value for /sort: argument
        /// </summary>
        /// <remarks>
        /// 0 = Name<br/>
        /// 1 = ClassLine<br/>
        /// 2 = CoveragePercentageAscending<br/>
        /// 3 = CoveragePercentageDescending<br/>
        /// 4 = UnvisitedSequencePointsAscending<br/>
        /// 5 = UnvisitedSequencePointsDescending<br/>
        /// 6 = VisitCountAscending<br/>
        /// 7 = VisitCountDescending<br/>
        /// 8 = FunctionCoverageAscending<br/>
        /// 9 = FunctionCoverageDescending<br/>
        /// </remarks>
        public string ReportOrder { get; set; }

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerToolKey, NCoverExplorerPath);

            if (!string.IsNullOrEmpty(Arguments))
            {
                yield return new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerToolArgsKey, Arguments);
            }

            if (!string.IsNullOrEmpty(ReportType))
            {
                yield return new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerReportTypeKey, ReportType);
            }

            if (!string.IsNullOrEmpty(ReportOrder))
            {
                yield return new DotNetCoverMessage(DotNetCoverMessage.NCoverExplorerReportOrderKey, ReportOrder);
            }
            yield return new ImportDataTeamCityMessage(ImportType.DotNetCoverage,
                                                       XmlReportPath,
                                                       DotNetCoverageTool.Ncover);
        }
    }
}