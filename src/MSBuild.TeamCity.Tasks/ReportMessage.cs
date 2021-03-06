/*
 * Created by: egr
 * Created at: 03.05.2009
 * � 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using Microsoft.Build.Framework;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks
{
    /// <summary>
    ///     Reports messages for build log
    /// </summary>
    /// <example>
    ///     Report message using NORMAL status (default)
    ///     <code><![CDATA[
    /// <ReportMessage Text="Message text" />
    /// ]]></code>
    ///     Report message using status specified
    ///     <code><![CDATA[
    /// <ReportMessage
    ///     Status="WARNING"
    ///     Text="Message text"
    /// />
    /// ]]></code>
    ///     Report message using status and error details specified (only ERROR mode)
    ///     <code><![CDATA[
    /// <ReportMessage
    ///     Status="ERROR"
    ///     ErrorDetails="Error details"
    ///     Text="Message text"
    /// />
    /// ]]></code>
    ///     Report message full example (with all optional attributes)
    ///     <code><![CDATA[
    /// <ReportMessage
    ///     IsAddTimestamp="true"
    ///     FlowId="1"
    ///     Status="ERROR"
    ///     ErrorDetails="Error details"
    ///     Text="Message text"
    /// />
    /// ]]></code>
    /// </example>
    public class ReportMessage : TeamCityTask
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportMessage" /> class
        /// </summary>
        public ReportMessage()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportMessage" /> class using
        ///     logger specified
        /// </summary>
        /// <param name="logger"><see cref="ILogger" /> implementation</param>
        public ReportMessage(ILogger logger)
            : base(logger)
        {
        }

        /// <summary>
        ///     Gets or sets message text
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets message status.
        ///     The status attribute may take following values: NORMAL, WARNING, FAILURE, ERROR. The default value is NORMAL.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Gets or sets error details text that is used only if status is ERROR, in other cases it is ignored.
        /// </summary>
        public string ErrorDetails { get; set; }

        /// <summary>
        ///     Reads TeamCity messages
        /// </summary>
        /// <returns>TeamCity messages list</returns>
        protected override IEnumerable<TeamCityMessage> ReadMessages()
        {
            yield return new ReportMessageBuilder(this.Text, this.Status, this.ErrorDetails).BuildMessage();
        }
    }
}