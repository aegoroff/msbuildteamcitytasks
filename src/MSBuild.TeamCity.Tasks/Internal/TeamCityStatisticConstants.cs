/*
 * Created by: egr
 * Created at: 16.10.2015
 * © 2007-2015 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Internal
{
    internal struct TeamCityStatisticConstants
    {
        internal const string CodeCoverageClassesCovered = "CodeCoverageAbsCCovered";
        internal const string CodeCoverageClassesTotal = "CodeCoverageAbsCTotal";
        internal const string CodeCoverageClassesPercent = "CodeCoverageC";

        internal const string CodeCoverageMethodsCovered = "CodeCoverageAbsMCovered";
        internal const string CodeCoverageMethodsTotal = "CodeCoverageAbsMTotal";
        internal const string CodeCoverageMethodsPercent = "CodeCoverageM";

        internal const string CodeCoverageLinesCovered = "CodeCoverageAbsLCovered";
        internal const string CodeCoverageLinesTotal = "CodeCoverageAbsLTotal";
        internal const string CodeCoverageLinesPercent = "CodeCoverageL";
    }
}