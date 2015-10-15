/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2015 Alexander Egorov
 */

using System;

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    ///     Represents import data message builder
    /// </summary>
    public class ImportDataMessageBuilder
    {
        private readonly ImportDataContext context;
        private readonly string findBugsHome;
        private readonly string tool;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImportDataMessageBuilder" /> class
        /// </summary>
        /// <param name="tool">
        ///     the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the
        ///     coverage settings.
        /// </param>
        /// <param name="context">Import context</param>
        /// <param name="findBugsHome">findBugsHome attribute specified pointing to the home directory oif installed FindBugs tool.</param>
        public ImportDataMessageBuilder(string tool, ImportDataContext context, string findBugsHome)
        {
            this.tool = tool;
            this.context = context;
            this.findBugsHome = findBugsHome;
        }

        /// <summary>
        ///     Builds message
        /// </summary>
        /// <returns>The new instance of <see cref="TeamCityMessage" /> class</returns>
        public TeamCityMessage BuildMessage()
        {
            if (string.IsNullOrEmpty(this.findBugsHome))
            {
                return string.IsNullOrEmpty(this.tool)
                    ? new ImportDataTeamCityMessage(this.context)
                    : new ImportDataTeamCityMessage(this.context, this.tool.ToDotNetCoverateTool());
            }
            if (this.context.Type != ImportType.FindBugs)
            {
                throw new NotSupportedException();
            }
            return new ImportDataTeamCityMessage(this.context, this.findBugsHome);
        }
    }
}