/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2013 Alexander Egorov
 */

using System;
using System.Diagnostics;

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents importData TeamCity message that imports data from third party sources.
    ///</summary>
    public class ImportDataTeamCityMessage : TeamCityMessage
    {
        /// <summary>
        ///  Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using <see cref="ImportType"/> value and path specified
        /// </summary>
        /// <param name="type">Data type. FxCop for example</param>
        /// <param name="path">Full path to file</param>
        /// <param name="verbose">Attribute will enable detailed logging into the build log.</param>
        public ImportDataTeamCityMessage(ImportType type, string path, bool verbose)
            : this(type.ImportTypeToString(), path, verbose)
        {
            if (type == ImportType.FindBugs)
            {
                throw new NotSupportedException("You must user another constructor");
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class for FindBugs report import
        /// </summary>
        /// <param name="type">Data type. Only FindBugs supportted</param>
        /// <param name="path">Full path to file</param>
        /// <param name="findBugsHome"></param>
        /// <param name="verbose">Attribute will enable detailed logging into the build log.</param>
        public ImportDataTeamCityMessage(ImportType type, string path, string findBugsHome, bool verbose)
            : this(type.ImportTypeToString(), path, verbose)
        {
            if (type != ImportType.FindBugs)
            {
                throw new NotSupportedException();
            }
            Attributes.Add("findBugsHome", findBugsHome);
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using type and path specified
        /// </summary>
        /// <param name="type">Data type. FxCop for example</param>
        /// <param name="path">Full path to file</param>
        /// <param name="verbose">Attribute will enable detailed logging into the build log.</param>
        public ImportDataTeamCityMessage(string type, string path, bool verbose)
        {
            Attributes.Add("type", type);
            Attributes.Add("path", path);
            if (verbose)
            {
                Attributes.Add("verbose", "true");
            }
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using type and path specified
        /// </summary>
        /// <param name="type">Data type. FxCop for example</param>
        /// <param name="path">Full path to file</param>
        /// <param name="tool">Here the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the coverage settings.</param>
        /// <param name="verbose">Attribute will enable detailed logging into the build log.</param>
        public ImportDataTeamCityMessage(ImportType type, string path, DotNetCoverageTool tool, bool verbose)
            : this(type, path, verbose)
        {
            if (type != ImportType.DotNetCoverage)
            {
                throw new NotSupportedException();
            }
            Attributes.Add(new MessageAttributeItem(tool.ToolToString(), "tool"));
        }

        /// <summary>
        /// Gets message name
        /// </summary>
        protected override string Message
        {
            [DebuggerStepThrough]
            get { return "importData"; }
        }
    }
}