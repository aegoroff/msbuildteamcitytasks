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
        private readonly string findBugsHome;
        private readonly bool verbose;

        /// <summary>
        ///  Initializes a new instance of the <see cref="ImportDataMessageBuilder"/> class
        /// </summary>
        /// <param name="tool">the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the coverage settings.</param>
        /// <param name="path">full path to data source file to import data from</param>
        /// <param name="type">imported data type.</param>
        /// <param name="findBugsHome">findBugsHome attribute specified pointing to the home directory oif installed FindBugs tool.</param>
        /// <param name="verbose">Attribute will enable detailed logging into the build log.</param>
        public ImportDataMessageBuilder(string tool, string path, string type, string findBugsHome, bool verbose = false)
        {
            this.tool = tool;
            this.path = path;
            this.type = type;
            this.findBugsHome = findBugsHome;
            this.verbose = verbose;
        }

        /// <summary>
        /// Builds message
        /// </summary>
        /// <returns>The new instance of <see cref="TeamCityMessage"/> class</returns>
        public TeamCityMessage BuildMessage()
        {
            if (!string.IsNullOrEmpty(findBugsHome))
            {
                return new ImportDataTeamCityMessage(ImportType.FindBugs, path, findBugsHome, verbose);
            }
            
            return string.IsNullOrEmpty(tool)
                       ? new ImportDataTeamCityMessage(type, path, this.verbose)
                       : new ImportDataTeamCityMessage(ImportType.DotNetCoverage,
                                                       path,
                                                       tool.ToDotNetCoverateTool(),
                                                       this.verbose);
        }
    }
}