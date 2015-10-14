/*
 * Created by: egr
 * Created at: 14.10.2015
 * © 2007-2015 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Internal
{
    internal class Summary
    {
        internal long NumSequencePoints { get; set; }

        internal long VisitedSequencePoints { get; set; }

        internal long VisitedClasses { get; set; }

        internal long NumClasses { get; set; }

        internal long VisitedMethods { get; set; }

        internal long NumMethods { get; set; }
    }
}