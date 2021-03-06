/*
 * Created by: egr
 * Created at: 02.09.2010
 * � 2007-2015 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents <see cref="ReportMessageTeamCityMessage" /> instance builder
    /// </summary>
    public class ReportMessageBuilder
    {
        private readonly string details;
        private readonly string status;
        private readonly string text;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportMessageBuilder" /> class
        /// </summary>
        /// <param name="text">Message text</param>
        /// <param name="status">
        ///     The status attribute may take following values: NORMAL, WARNING, FAILURE, ERROR. The default value
        ///     is NORMAL.
        /// </param>
        /// <param name="details">error details text that is used only if status is ERROR</param>
        public ReportMessageBuilder(string text, string status, string details)
        {
            this.text = text;
            this.status = status;
            this.details = details;
        }

        /// <summary>
        ///     Builds message
        /// </summary>
        /// <returns>The new instance of <see cref="TeamCityMessage" /> class</returns>
        public TeamCityMessage BuildMessage()
        {
            if (!string.IsNullOrEmpty(this.status) && !string.IsNullOrEmpty(this.details))
            {
                return new ReportMessageTeamCityMessage(this.text, this.status, this.details);
            }
            if (!string.IsNullOrEmpty(this.status))
            {
                return new ReportMessageTeamCityMessage(this.text, this.status);
            }
            return new ReportMessageTeamCityMessage(this.text);
        }
    }
}