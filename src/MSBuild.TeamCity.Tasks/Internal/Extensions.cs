/*
 * Created by: egr
 * Created at: 14.10.2010
 * © 2007-2010 Alexander Egorov
 */

using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    /// Represents internal useful extensions
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Concatenates a specified separator between each element of a specified enumerable, 
        /// yielding a single concatenated string. 
        /// </summary>
        /// <param name="enumerable">Sequence to concatenate</param>
        /// <param name="separator">Separator between items</param>
        /// <returns>concatenated string</returns>
        internal static string Join( this IEnumerable<string> enumerable, string separator )
        {
            return string.Join(separator, enumerable.ToArray());
        }

        internal static void AddRange( this IList<string> list, ITaskItem[] items )
        {
            if ( items == null )
            {
                return;
            }
            foreach ( string include in items.Select(item => item.ItemSpec) )
            {
                list.Add(include);
            }
        }
    }
}