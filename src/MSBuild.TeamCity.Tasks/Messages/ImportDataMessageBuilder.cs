/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2013 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks.Messages
{
    /// <summary>
    /// Represents import data message builder
    /// </summary>
    public class ImportDataMessageBuilder
    {
        private readonly string tool;
        private readonly string path;
        private readonly string type;

        ///<summary>
        /// Initializes a new instance of the <see cref="ImportDataMessageBuilder"/> class
        ///</summary>
        ///<param name="tool">the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the coverage settings.</param>
        ///<param name="path">full path to data source file to import data from</param>
        ///<param name="type">imported data type.</param>
        public ImportDataMessageBuilder(string tool, string path, string type)
        {
            this.tool = tool;
            this.path = path;
            this.type = type;
        }

        /// <summary>
        /// Builds message
        /// </summary>
        /// <returns>The new instance of <see cref="TeamCityMessage"/> class</returns>
        public TeamCityMessage BuildMessage()
        {
            return string.IsNullOrEmpty(tool)
                       ? new ImportDataTeamCityMessage(type, path)
                       : new ImportDataTeamCityMessage(ImportType.DotNetCoverage,
                                                       path,
                                                       tool.ToDotNetCoverateTool());
        }
    }
}