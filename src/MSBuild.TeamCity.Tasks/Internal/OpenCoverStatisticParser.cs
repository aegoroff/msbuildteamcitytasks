/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2013 Alexander Egorov
 */

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Parses OpenCover results and generated TC statistic messages
    /// </summary>
    public class OpenCoverStatisticParser
    {
        #region Constants and Fields

        private static readonly Regex classRegex =
            new Regex(@"^\s*Visited\s+Classes\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                      RegexOptions.Compiled);

        private static readonly Regex methodRegex =
            new Regex(@"^\s*Visited\s+Methods\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                      RegexOptions.Compiled);

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Parses input and generates <see cref="TeamCityMessage"/> statistic
        /// </summary>
        /// <param name="input">OpenCover console results</param>
        /// <returns><see cref="TeamCityMessage"/> stream</returns>
        public IEnumerable<TeamCityMessage> Parse(IEnumerable<string> input)
        {
            foreach (string line in input)
            {
                foreach (
                    var teamCityMessage in
                        TeamCityMessages(line, classRegex, "CodeCoverageAbsCCovered", "CodeCoverageAbsCTotal",
                                         "CodeCoverageC")) yield return teamCityMessage;
                foreach (
                    var teamCityMessage in
                        TeamCityMessages(line, methodRegex, "CodeCoverageAbsMCovered", "CodeCoverageAbsMTotal",
                                         "CodeCoverageM")) yield return teamCityMessage;
            }
        }

        #endregion

        #region Methods

        private static IEnumerable<TeamCityMessage> TeamCityMessages(string line, Regex rx, string coveredParam,
                                                                     string totalParam, string percentParam)
        {
            var classMatch = rx.Match(line);
            if (!classMatch.Success)
            {
                yield break;
            }
            var covered = classMatch.Groups[1].Value;
            var total = classMatch.Groups[2].Value;
            var percent = classMatch.Groups[3].Value;
            yield return
                new BuildStatisticTeamCityMessage(coveredParam,
                                                  float.Parse(covered, CultureInfo.InvariantCulture));
            yield return
                new BuildStatisticTeamCityMessage(totalParam,
                                                  float.Parse(total, CultureInfo.InvariantCulture));
            yield return
                new BuildStatisticTeamCityMessage(percentParam,
                                                  float.Parse(percent, CultureInfo.InvariantCulture));
        }

        #endregion
    }
}