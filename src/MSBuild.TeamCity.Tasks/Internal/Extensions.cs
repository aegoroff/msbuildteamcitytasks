/*
 * Created by: egr
 * Created at: 14.10.2010
 * © 2007-2015 Alexander Egorov
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;

namespace MSBuild.TeamCity.Tasks.Internal
{
    /// <summary>
    ///     Represents internal useful extensions
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        ///     Concatenates a specified separator between each element of a specified enumerable,
        ///     yielding a single concatenated string.
        /// </summary>
        /// <param name="enumerable">Sequence to concatenate</param>
        /// <param name="separator">Separator between items</param>
        /// <returns>concatenated string</returns>
        internal static string Join(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        internal static void AddRange(this IList<string> list, IEnumerable<ITaskItem> items)
        {
            if (items == null)
            {
                return;
            }
            list.AddRange(items.Select(item => item.ItemSpec));
        }

        internal static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (var include in items)
            {
                list.Add(include);
            }
        }

        internal static string GetDirectoryName(this string path)
        {
            return Path.GetDirectoryName(Path.GetFullPath(path));
        }

        internal static IList<string> ReadLines(this StreamReader reader)
        {
            var result = new List<string>();

            while (!reader.EndOfStream)
            {
                result.Add(reader.ReadLine());
            }

            return result;
        }
    }
}