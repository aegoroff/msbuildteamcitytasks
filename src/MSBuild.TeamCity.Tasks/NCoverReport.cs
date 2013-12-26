/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2013 Alexander Egorov
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
        public NCoverReport(ILogger logger)
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
        /// Gets or sets a value indicating whether to enable detailed logging into the build log. False by default
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets action that will change output level if no reports matching the path specified were found.<p/>
        /// May take the following values: info (default), nothing, warning, error
        /// </summary>
        public string WhenNoDataPublished { get; set; }

        /// <summary>
        /// Gets or sets whether process all the files matching the path. Otherwise, only those updated during the build (is determined by 
        /// last modification timestamp) are processed. False by default
        /// </summary>
        public bool ParseOutOfDate { get; set; }

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
            var context = new ImportDataContext
            {
                Type = ImportType.DotNetCoverage,
                Path = XmlReportPath,
                Verbose = Verbose,
                ParseOutOfDate = ParseOutOfDate,
                WhenNoDataPublished = WhenNoDataPublished
            };
            yield return new ImportDataTeamCityMessage(context, DotNetCoverageTool.Ncover);
        }
    }
}