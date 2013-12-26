/*
 * Created by: egr
 * Created at: 07.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    ///<summary>
    /// Manually configures .NET coverage processing using NCover3 tool.
    /// See http://confluence.jetbrains.net/display/TCD5/Manually+Configuring+Reporting+Coverage
    ///</summary>
    /// <example>Configures .NET coverage processing using NCover3 tool
    /// <code><![CDATA[
    /// <NCover3Report
    ///     ToolPath="C:\Program Files\NCover3"
    ///     XmlReportPath="D:\project\ncover3.xml"
    /// />
    /// ]]></code>
    /// Configures .NET coverage processing using NCover3 tool and specifying arguments for NCover report generator
    /// <code><![CDATA[
    /// <NCover3Report
    ///     ToolPath="C:\Program Files\NCover3"
    ///     XmlReportPath="D:\project\ncover3.xml"
    ///     Arguments="FullCoverageReport:Html:{teamcity.report.path}"
    /// />
    /// ]]></code>
    /// </example>
    public class NCover3Report : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="NCover3Report"/> class
        ///</summary>
        public NCover3Report()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="NCover3Report"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public NCover3Report(ILogger logger)
            : base(logger)
        {
        }

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
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new DotNetCoverMessage(DotNetCoverMessage.NCover3HomeKey, ToolPath);
            if (!string.IsNullOrEmpty(Arguments))
            {
                yield return new DotNetCoverMessage(DotNetCoverMessage.NCover3ReporterArgsKey, Arguments);
            }
            var context = new ImportDataContext
            {
                Type = ImportType.DotNetCoverage,
                Path = XmlReportPath,
                Verbose = Verbose,
                ParseOutOfDate = ParseOutOfDate,
                WhenNoDataPublished = WhenNoDataPublished
            };
            yield return new ImportDataTeamCityMessage(context, DotNetCoverageTool.Ncover3);
        }
    }
}