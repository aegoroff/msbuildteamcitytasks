/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents importData TeamCity message that imports data from third party sources.
    /// </summary>
    public class ImportDataTeamCityMessage : TeamCityMessage
    {
        private static readonly HashSet<string> actionsWhenNoDataPublished = new HashSet<string>
        {
            "info",
            "nothing",
            "warning",
            "error"
        };

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImportDataTeamCityMessage" /> class for FindBugs report import
        /// </summary>
        /// <param name="context">Import context</param>
        /// <param name="findBugsHome">findBugsHome attribute specified pointing to the home directory oif installed FindBugs tool.</param>
        public ImportDataTeamCityMessage(ImportDataContext context, string findBugsHome)
            : this(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (context.Type != ImportType.FindBugs)
            {
                throw new NotSupportedException();
            }
            this.Attributes.Add("findBugsHome", findBugsHome);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImportDataTeamCityMessage" /> class using type and path specified
        /// </summary>
        /// <param name="context">Import context</param>
        public ImportDataTeamCityMessage(ImportDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            this.Attributes.Add("type", context.Type.ImportTypeToString());
            this.Attributes.Add("path", context.Path);
            if (context.Verbose)
            {
                this.Attributes.Add("verbose", "true");
            }
            if (context.ParseOutOfDate)
            {
                this.Attributes.Add("parseOutOfDate", "true");
            }
            if (string.IsNullOrWhiteSpace(context.WhenNoDataPublished))
            {
                return;
            }
            if (!actionsWhenNoDataPublished.Contains(context.WhenNoDataPublished))
            {
                throw new NotSupportedException("Action not supportted. Only " +
                                                string.Join(", ", actionsWhenNoDataPublished) + " actions allowed.");
            }
            this.Attributes.Add("whenNoDataPublished", context.WhenNoDataPublished);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImportDataTeamCityMessage" /> class using type and path specified
        /// </summary>
        /// <param name="context">Import context</param>
        /// <param name="tool">
        ///     Here the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool
        ///     in the coverage settings.
        /// </param>
        public ImportDataTeamCityMessage(ImportDataContext context, DotNetCoverageTool tool)
            : this(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (context.Type != ImportType.DotNetCoverage)
            {
                throw new NotSupportedException();
            }
            this.Attributes.Add(new MessageAttributeItem(tool.ToolToString(), "tool"));
        }

        /// <summary>
        ///     Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough] get { return "importData"; }
        }
    }
}