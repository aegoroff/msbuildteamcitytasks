/*
 * Created by: egr
 * Created at: 12.09.2010
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;

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

        internal static string ImportTypeToString(this ImportType type)
        {
            switch (type)
            {
                case ImportType.Junit:
                    return "junit";
                case ImportType.Surefire:
                    return "surefire";
                case ImportType.Nunit:
                    return "nunit";
                case ImportType.FindBugs:
                    return "findBugs";
                case ImportType.Pmd:
                    return "pmd";
                case ImportType.FxCop:
                    return "FxCop";
                case ImportType.DotNetCoverage:
                    return "dotNetCoverage";
                case ImportType.Mstest:
                    return "mstest";
                case ImportType.Gtest:
                    return "gtest";
                case ImportType.Jslint:
                    return "jslint";
                case ImportType.CheckStyle:
                    return "checkstyle";
                case ImportType.PmdCpd:
                    return "pmdCpd";
                case ImportType.ReSharperDupFinder:
                    return "ReSharperDupFinder";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}