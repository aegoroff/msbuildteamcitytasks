/*
 * Created by: egr
 * Created at: 09.03.2012
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using MSBuild.TeamCity.Tasks.Messages;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Parses OpenCover results and generated TC statistic messages
    /// </summary>
    public class OpenCoverStatisticParser
    {
        private const string VisitedClassesAttr = "visitedClasses";
        private const string TotalClassesAttr = "numClasses";
        private const string VisitedMethodsAttr = "visitedMethods";
        private const string TotalMethodsAttr = "numMethods";
        private const string VisitedPointsAttr = "visitedSequencePoints";
        private const string TotalPointsAttr = "numSequencePoints";

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
            yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsCCovered", summary.VisitedClasses);
            yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsCTotal", summary.NumClasses);
            yield return
                new BuildStatisticTeamCityMessage("CodeCoverageC",
                    (summary.VisitedClasses/(float) summary.NumClasses)*100);

            yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsMCovered", summary.VisitedMethods);
            yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsMTotal", summary.NumMethods);
            yield return
                new BuildStatisticTeamCityMessage("CodeCoverageM",
                    (summary.VisitedMethods/(float) summary.NumMethods)*100);

            yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsLCovered", summary.VisitedSequencePoints);
            yield return new BuildStatisticTeamCityMessage("CodeCoverageAbsLTotal", summary.NumSequencePoints);
            yield return
                new BuildStatisticTeamCityMessage("CodeCoverageL",
                    (summary.VisitedSequencePoints/(float) summary.NumSequencePoints)*100);
        }

        private static void ReadStatisticIntoSummary(XmlReader reader, Summary summary)
        {
            CreateMessage(reader, VisitedClassesAttr, l => summary.VisitedClasses = l);
            CreateMessage(reader, TotalClassesAttr, l => summary.NumClasses = l);
            CreateMessage(reader, VisitedMethodsAttr, l => summary.VisitedMethods = l);
            CreateMessage(reader, TotalMethodsAttr, l => summary.NumMethods = l);
            CreateMessage(reader, VisitedPointsAttr, l => summary.VisitedSequencePoints = l);
            CreateMessage(reader, TotalPointsAttr, l => summary.NumSequencePoints = l);
        }

        private static void CreateMessage(XmlReader reader, string attribute, Action<long> ifSuccess)
        {
            if (reader.Name == attribute)
            {
                ifSuccess(long.Parse(reader.Value, CultureInfo.InvariantCulture));
            }
        }

        #endregion
    }
}