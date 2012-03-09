/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2012 Alexander Egorov
 */

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using MSBuild.TeamCity.Tasks.Internal;
using MSBuild.TeamCity.Tasks.Messages;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    /// Runs code coverage using OpenCover tool (https://github.com/sawilde/opencover) and exports results into TC.
    /// </summary>
    public class RunOpenCoverage : TeamCityTask
    {
        #region Constants and Fields

        private const string OpenCoverConsole = "opencover.console.exe";

        private static readonly Regex classRegex = new Regex(@".*Visited\s+Classes\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                                                    RegexOptions.Compiled);

        private static readonly Regex methodRegex = new Regex(@".*Visited\s+Methods\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                                                    RegexOptions.Compiled);

        #endregion

        #region Constructors and Destructors

        ///<summary>
        /// Initializes a new instance of the <see cref="RunOpenCoverage"/> class
        ///</summary>
        public RunOpenCoverage()
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="RunOpenCoverage"/> class using 
        /// logger specified
        ///</summary>
        ///<param name="logger"><see cref="ILogger"/> implementation</param>
        public RunOpenCoverage(ILogger logger)
            : base(logger)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets coverage filter
        /// </summary>
        /// <remarks>
        /// A list of filters to apply to selectively include or exclude assemblies and classes from coverage results. 
        /// Filters have their own format ±[assembly-filter]class-filter. 
        /// If no filter(s) are supplied then a default include all filter is applied +[*]*. 
        /// As can be seen you can use an * as a wildcard. Also an exclusion filter (-) takes precedence over an inclusion filter (+).
        /// </remarks>
        public string Filter { get; set; }

        /// <summary>
        /// Gets or sets arguments for target process
        /// </summary>
        [Required]
        public string TargetArguments { get; set; }

        /// <summary>
        /// Gets or sets full path to path to executable file to count coverage
        /// </summary>
        [Required]
        public string TargetPath { get; set; }

        /// <summary>
        /// Gets or sets path to working directory to target process
        /// </summary>
        public string TargetWorkDir { get; set; }

        /// <summary>
        /// Gets or sets full path to PartCover installation folder
        /// </summary>
        [Required]
        public string ToolPath { get; set; }

        /// <summary>
        /// Gets or sets full path to xml report file that was created by OpenCover
        /// </summary>
        [Required]
        public string XmlReportPath { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            var commandLine = new OpenCoverCommandLine
                                  {
                                      Target = TargetPath,
                                      TargetWorkDir = TargetWorkDir,
                                      TargetArguments = TargetArguments,
                                      Filter = Filter,
                                      Output = XmlReportPath,
                                  };

            string openCoverExePath = Path.Combine(ToolPath, OpenCoverConsole);
            var runner = new ProcessRunner(openCoverExePath) {RedirectStandardOutput = true};
            IList<string> result = runner.Run(commandLine.ToString());
            foreach (var line in result)
            {
                var classMatch = classRegex.Match(line);
                if (classMatch.Success)
                {
                    var covered = classMatch.Groups[1].Value;
                    var total = classMatch.Groups[2].Value;
                    var percent = classMatch.Groups[3].Value;
                    yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsCCovered", float.Parse(covered, CultureInfo.InvariantCulture));
                    yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsCTotal", float.Parse(total, CultureInfo.InvariantCulture));
                    yield return new BuildStatisticTeamCityMessage("CodeCoverageC", float.Parse(percent, CultureInfo.InvariantCulture));
                }
                var methodMatch = methodRegex.Match(line);
                if (methodMatch.Success)
                {
                    var covered = methodMatch.Groups[1].Value;
                    var total = methodMatch.Groups[2].Value;
                    var percent = methodMatch.Groups[3].Value;
                    yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsMCovered", float.Parse(covered, CultureInfo.InvariantCulture));
                    yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsMTotal", float.Parse(total, CultureInfo.InvariantCulture));
                    yield return new BuildStatisticTeamCityMessage("CodeCoverageM", float.Parse(percent, CultureInfo.InvariantCulture));
                }
            }
        }

        #endregion
    }
}