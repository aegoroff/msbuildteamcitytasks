/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Runs code coverage using OpenCover tool (https://github.com/sawilde/opencover) and exports results into TC.
    /// </summary>
    public class RunOpenCoverage : TeamCityTask
    {
        #region Constants and Fields

        private const string OpenCoverConsole = "opencover.console.exe";

        #endregion

        #region Methods

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            var commandLine = new OpenCoverCommandLine
            {
                Target = this.TargetPath,
                TargetWorkDir = this.TargetWorkDir,
                TargetArguments = this.TargetArguments,
                Output = this.XmlReportPath,
                HideSkipped = this.HideSkipped,
                ExcludeByfile = this.ExcludeByfile,
                SkipAutoProps = this.SkipAutoProps
            };
            commandLine.Filter.AddRange(this.Filter);

            var openCoverExePath = Path.Combine(this.ToolPath, OpenCoverConsole);
            var runner = new ProcessRunner(openCoverExePath) { RedirectStandardOutput = true };
            var result = runner.Run(commandLine.ToString());
            this.Logger.LogMessage(MessageImportance.Normal, string.Join(Environment.NewLine, result));
            if (!File.Exists(this.XmlReportPath))
            {
                return new TeamCityMessage[0];
            }
            var parser = new OpenCoverStatisticParser();
            return parser.Parse(this.XmlReportPath);
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RunOpenCoverage" /> class
        /// </summary>
        public RunOpenCoverage()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RunOpenCoverage" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public RunOpenCoverage(ILogger logger)
            : base(logger)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets coverage filter
        /// </summary>
        /// <remarks>
        ///     A list of filters to apply to selectively include or exclude assemblies and classes from coverage results.
        ///     Filters have their own format ±[assembly-filter]class-filter.
        ///     If no filter(s) are supplied then a default include all filter is applied +[*]*.
        ///     As can be seen you can use an * as a wildcard. Also an exclusion filter (-) takes precedence over an inclusion
        ///     filter (+).
        /// </remarks>
        public ITaskItem[] Filter { get; set; }

        /// <summary>
        ///     Gets or sets exclude by flie filter
        /// </summary>
        /// <remarks>
        ///     Exclude a class (or methods) by filter(s) that match the filenames. An * can be used as a wildcard.
        /// </remarks>
        public string ExcludeByfile { get; set; }

        /// <summary>
        ///     Gets or sets whether to remove information from output file
        /// </summary>
        /// <summary>
        ///     File|Filter|Attribute|MissingPdb| MissingPdb |All [;File|Filter|Attribute|MissingPdb| MissingPdb |All
        /// </summary>
        /// <remarks>
        ///     Remove information from output file (-output:) that relates to classes/modules that have been skipped (filtered)
        ///     due to the use of the following switches –excludebyfile:,  excludebyattribute: and –filter: or where the PDB is
        ///     missing.
        /// </remarks>
        public string HideSkipped { get; set; }

        /// <summary>
        ///     Gets or set whether to Neither track nor record Auto-Implemented properties.
        /// </summary>
        /// <remarks>
        /// i.e. skip getters and setters like these
        /// <code><![CDATA[
        /// public bool Service { get; set; }
        /// ]]></code>
        /// </remarks>
        public bool SkipAutoProps { get; set; }

        /// <summary>
        ///     Gets or sets arguments for target process
        /// </summary>
        [Required]
        public string TargetArguments { get; set; }

        /// <summary>
        ///     Gets or sets full path to path to executable file to count coverage
        /// </summary>
        [Required]
        public string TargetPath { get; set; }

        /// <summary>
        ///     Gets or sets path to working directory to target process
        /// </summary>
        public string TargetWorkDir { get; set; }

        /// <summary>
        ///     Gets or sets full path to PartCover installation folder
        /// </summary>
        [Required]
        public string ToolPath { get; set; }

        /// <summary>
        ///     Gets or sets full path to xml report file that was created by OpenCover
        /// </summary>
        [Required]
        public string XmlReportPath { get; set; }

        #endregion
    }
}