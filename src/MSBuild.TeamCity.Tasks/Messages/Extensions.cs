/*
 * Created by: egr
 * Created at: 12.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    /// Represents messages useful extensions
    /// </summary>
    internal static class Extensions
    {
        private const string PartCover = "partcover";
        private const string Ncover = "ncover";
        private const string Ncover3 = "ncover3";

        internal static void Add(this IList<MessageAttributeItem> list, string name, string value)
        {
            var item = new MessageAttributeItem(value, name);

            if (list.Contains(item))
            {
                list.Remove(item);
            }
            list.Add(item);
        }

        internal static string ToolToString(this DotNetCoverageTool tool)
        {
            switch (tool)
            {
                case DotNetCoverageTool.PartCover:
                    return PartCover;
                case DotNetCoverageTool.Ncover:
                    return Ncover;
                case DotNetCoverageTool.Ncover3:
                    return Ncover3;
                default:
                    throw new NotSupportedException();
            }
        }

        internal static DotNetCoverageTool ToDotNetCoverateTool(this string str)
        {
            switch (str)
            {
                case PartCover:
                    return DotNetCoverageTool.PartCover;
                case Ncover:
                    return DotNetCoverageTool.Ncover;
                case Ncover3:
                    return DotNetCoverageTool.Ncover3;
                default:
                    throw new NotSupportedException();
            }
        }

        private static readonly Dictionary<string, ImportType> types = new Dictionary<string, ImportType>
        {
            { "junit", ImportType.Junit },
            { "surefire", ImportType.Surefire },
            { "nunit", ImportType.Nunit },
            { "findBugs", ImportType.FindBugs },
            { "pmd", ImportType.Pmd },
            { "FxCop", ImportType.FxCop },
            { "dotNetCoverage", ImportType.DotNetCoverage },
            { "mstest", ImportType.Mstest },
            { "gtest", ImportType.Gtest },
            { "jslint", ImportType.Jslint },
            { "checkstyle", ImportType.CheckStyle },
            { "pmdCpd", ImportType.PmdCpd },
            { "ReSharperDupFinder", ImportType.ReSharperDupFinder },
        };

        internal static ImportType ToImportType(this string type)
        {
            if (!types.ContainsKey(type))
            {
                throw new NotSupportedException();
            }
            return types[type];
        }

        private static TKey FindKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            foreach (var pair in dictionary.Where(pair => value.Equals(pair.Value)))
            {
                return pair.Key;
            }
            return default(TKey);
        }

        internal static string ImportTypeToString(this ImportType type)
        {
            return types.FindKeyByValue(type);
        }
    }
}