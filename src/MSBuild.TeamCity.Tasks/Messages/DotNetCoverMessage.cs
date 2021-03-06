/*
 * Created by: egr
 * Created at: 07.09.2010
 * � 2007-2015 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents dotNetCoverage TeamCity message
    /// </summary>
    public class DotNetCoverMessage : TeamCityMessage
    {
        internal const string NCover3HomeKey = "ncover3_home";
        internal const string NCover3ReporterArgsKey = "ncover3_reporter_args";
        internal const string NCoverExplorerToolKey = "ncover_explorer_tool";
        internal const string NCoverExplorerToolArgsKey = "ncover_explorer_tool_args";
        internal const string NCoverExplorerReportTypeKey = "ncover_explorer_report_type";
        internal const string NCoverExplorerReportOrderKey = "ncover_explorer_report_order";
        internal const string PartcoverReportXsltsKey = "partcover_report_xslts";

        private readonly Dictionary<string, int> validKeys = new Dictionary<string, int>(CreateValidKeys());

        /// <summary>
        ///     Initializes a new instance of the <see cref="DotNetCoverMessage" /> class
        /// </summary>
        /// <param name="key">Key's name</param>
        /// <param name="value">Parameter value</param>
        /// <exception cref="ArgumentException">
        ///     Occurs in case of invalid key name
        /// </exception>
        public DotNetCoverMessage(string key, string value)
        {
            if (!this.validKeys.ContainsKey(key))
            {
                throw new ArgumentException("Invalid key name.", nameof(key));
            }
            this.Key = key;
            this.Value = value;
            this.Attributes.Add(this.Key, this.Value);
        }

        /// <summary>
        ///     Gets parameter value
        /// </summary>
        public string Key { get; }

        /// <summary>
        ///     Gets parameter key name
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "dotNetCoverage"; }
        }

        private static Dictionary<string, int> CreateValidKeys()
        {
            var result = new Dictionary<string, int>
            {
                { NCover3HomeKey, 1 },
                { NCover3ReporterArgsKey, 1 },
                { NCoverExplorerToolKey, 1 },
                { NCoverExplorerToolArgsKey, 1 },
                { NCoverExplorerReportTypeKey, 1 },
                { NCoverExplorerReportOrderKey, 1 },
                { PartcoverReportXsltsKey, 1 }
            };
            return result;
        }
    }
}