/*
 * Created by: egr
 * Created at: 03.05.2009
 * © 2007-2012 Alexander Egorov
 */

using System;

namespace MSBuild.TeamCity.Tasks.Messages
{
    ///<summary>
    /// Represents importData TeamCity message that imports data from third party sources.
    ///</summary>
    public class ImportDataTeamCityMessage : TeamCityMessage
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using <see cref="ImportType"/> value and path specified
        ///</summary>
        ///<param name="type">Data type. FxCop for example</param>
        ///<param name="path">Full path to file</param>
        public ImportDataTeamCityMessage(ImportType type, string path) : this(type.ImportTypeToString(), path)
        {
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using type and path specified
        ///</summary>
        ///<param name="type">Data type. FxCop for example</param>
        ///<param name="path">Full path to file</param>
        public ImportDataTeamCityMessage(string type, string path)
        {
            Attributes.Add("type", type);
            Attributes.Add("path", path);
        }

        ///<summary>
        /// Initializes a new instance of the <see cref="ImportDataTeamCityMessage"/> class using type and path specified
        ///</summary>
        ///<param name="type">Data type. FxCop for example</param>
        ///<param name="path">Full path to file</param>
        ///<param name="tool">Here the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the coverage settings.</param>
        public ImportDataTeamCityMessage(ImportType type, string path, DotNetCoverageTool tool) : this(type, path)
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
            get { return "importData"; }
        }
    }
}