/*
 * Created by: egr
 * Created at: 08.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Manually configures .NET coverage processing using PartCover tool.
    /// See http://confluence.jetbrains.net/display/TCD5/Manually+Configuring+Reporting+Coverage
    /// </summary>
    /// <example>Configures .NET coverage processing using PartCover tool
    /// <code><![CDATA[
    /// <PartCoverReport
    ///     XmlReportPath="D:\project\partcover.xml"
    /// />
    /// ]]></code>
    /// Configures .NET coverage processing using PartCover tool and xslt transformation rules
    /// <code><![CDATA[
    /// <PartCoverReport
    ///     ReportXslts="file.xslt=>generatedFileName.html;file1.xslt=>generatedFileName1.html"
    ///     XmlReportPath="D:\project\partcover.xml"
    /// />
    /// ]]></code>
    /// </example>
    public class PartCoverReport : TeamCityTask
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="PartCoverReport"/> class
        ///</summary>
        public PartCoverReport()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="PartCoverReport"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public PartCoverReport(ILogger logger)
            : base(logger)
        {
        }

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
            if (ReportXslts != null)
            {
                yield return
                    new DotNetCoverMessage(DotNetCoverMessage.PartcoverReportXsltsKey,
                                           ReportXslts.Select(report => report.ItemSpec).Join("\n"));
            }
            var context = new ImportDataContext
            {
                Type = ImportType.DotNetCoverage,
                Path = XmlReportPath,
                Verbose = Verbose,
                ParseOutOfDate = ParseOutOfDate,
                WhenNoDataPublished = WhenNoDataPublished
            };
            yield return
                new ImportDataTeamCityMessage(context, DotNetCoverageTool.PartCover);
        }
    }
}