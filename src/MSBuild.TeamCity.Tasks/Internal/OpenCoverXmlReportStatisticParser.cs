/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2015 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Parses OpenCover xml report and generated TC statistic messages
    /// </summary>
    public class OpenCoverXmlReportStatisticParser
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Parses input and generates <see cref="TeamCityMessage" /> statistic
        /// </summary>
        /// <param name="reportPath">Full path to OpenCover xml report</param>
        /// <returns><see cref="TeamCityMessage" /> stream</returns>
        public IEnumerable<TeamCityMessage> Parse(string reportPath)
        {
            var summary = new Summary();
            using (var reader = XmlReader.Create(reportPath))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    if (reader.Name != "Summary")
                    {
                        continue;
                    }
                    reader.MoveToFirstAttribute();
                    ReadStatisticIntoSummary(reader, summary);
                    while (reader.MoveToNextAttribute())
                    {
                        ReadStatisticIntoSummary(reader, summary);
                    }
                    break;
                }
            }
            yield return new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageClassesCovered, summary.VisitedClasses);
            yield return new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageClassesTotal, summary.NumClasses);
            yield return
                new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageClassesPercent,
                    (summary.VisitedClasses/(float) summary.NumClasses)*100);

            yield return new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageMethodsCovered, summary.VisitedMethods);
            yield return new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageMethodsTotal, summary.NumMethods);
            yield return
                new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageMethodsPercent,
                    (summary.VisitedMethods/(float) summary.NumMethods)*100);

            yield return new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageLinesCovered, summary.VisitedSequencePoints);
            yield return new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageLinesTotal, summary.NumSequencePoints);
            yield return
                new BuildStatisticTeamCityMessage(TeamCityStatisticConstants.CodeCoverageLinesPercent,
                    (summary.VisitedSequencePoints/(float) summary.NumSequencePoints)*100);
        }

        private static void ReadStatisticIntoSummary(XmlReader reader, Summary summary)
        {
            var actions = new Dictionary<string, Action<long>>
            {
                { "visitedClasses", l => summary.VisitedClasses = l },
                { "numClasses", l => summary.NumClasses = l },
                { "visitedMethods", l => summary.VisitedMethods = l },
                { "numMethods", l => summary.NumMethods = l },
                { "visitedSequencePoints", l => summary.VisitedSequencePoints = l },
                { "numSequencePoints", l => summary.NumSequencePoints = l }
            };
            if (actions.ContainsKey(reader.Name))
            {
                actions[reader.Name](long.Parse(reader.Value, CultureInfo.InvariantCulture));
            }
        }

        #endregion
    }
}