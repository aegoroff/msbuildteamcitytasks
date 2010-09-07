/*
 * Created by: egr
 * Created at: 02.09.2010
 * © 2007-2010 Alexander Egorov
 */

namespace MSBuild.TeamCity.Tasks
{
	/// <summary>
	/// Represents import data message builder
	/// </summary>
	public class ImportDataBuilder
	{
		private readonly string _tool;
		private readonly string _path;
		private readonly string _type;

		///<summary>
		/// Initializes a new instance of the <see cref="ImportDataBuilder"/> class
		///</summary>
		///<param name="tool">the tool name value can be partcover, ncover, or ncover3, depending on selected coverage tool in the coverage settings.</param>
		///<param name="path">full path to data source file to import data from</param>
		///<param name="type">imported data type.</param>
		public ImportDataBuilder( string tool, string path, string type )
		{
			_tool = tool;
			_path = path;
			_type = type;
		}

		/// <summary>
		/// Builds message
		/// </summary>
		/// <returns>The new instance of <see cref="TeamCityMessage"/> class</returns>
		public TeamCityMessage BuildMessage()
		{
			return string.IsNullOrEmpty(_tool)
			       	? new ImportDataTeamCityMessage(_type, _path)
			       	: new ImportDataTeamCityMessage(ImportType.DotNetCoverage,
			       	                                _path,
			       	                                ImportDataTeamCityMessage.ToDotNetCoverateTool(_tool));
		}
	}
}