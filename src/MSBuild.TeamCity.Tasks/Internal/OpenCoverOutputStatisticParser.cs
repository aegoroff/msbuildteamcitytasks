/*
 * Created by: egr
 * Created at: 16.10.2015
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Parses OpenCover command lint output and generated TC statistic messages
    /// </summary>
    public class OpenCoverOutputStatisticParser
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Parses input and generates <see cref="TeamCityMessage" /> statistic
        /// </summary>
        /// <param name="input">OpenCover console results</param>
        /// <returns><see cref="TeamCityMessage" /> stream</returns>
        public IEnumerable<TeamCityMessage> Parse(IEnumerable<string> input)
        {
            var classTuple = new Tuple<Regex, string, string, string>(classRegex,
                TeamCityStatisticConstants.CodeCoverageClassesCovered,
                TeamCityStatisticConstants.CodeCoverageClassesTotal,
                TeamCityStatisticConstants.CodeCoverageClassesPercent);

            var methodTuple = new Tuple<Regex, string, string, string>(methodRegex,
                TeamCityStatisticConstants.CodeCoverageMethodsCovered,
                TeamCityStatisticConstants.CodeCoverageMethodsTotal,
                TeamCityStatisticConstants.CodeCoverageMethodsPercent);

            var pointsTuple = new Tuple<Regex, string, string, string>(pointsRegex,
                TeamCityStatisticConstants.CodeCoverageLinesCovered,
                TeamCityStatisticConstants.CodeCoverageLinesTotal,
                TeamCityStatisticConstants.CodeCoverageLinesPercent);

            var parsingData = new[]
            {
                classTuple, methodTuple, pointsTuple
            };

            return from line in input
                from tuple in parsingData
                from teamCityMessage in TeamCityMessages(line, tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4)
                select teamCityMessage;
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

        #region Constants and Fields

        private static readonly Regex classRegex =
            new Regex(@"^\s*Visited\s+Classes\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                RegexOptions.Compiled);

        private static readonly Regex methodRegex =
            new Regex(@"^\s*Visited\s+Methods\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                RegexOptions.Compiled);

        private static readonly Regex pointsRegex =
            new Regex(@"^\s*Visited\s+Points\s+(\d+)\s+of\s+(\d+)\s+\((\d+(\.\d+)*)\)",
                RegexOptions.Compiled);

        #endregion
    }
}